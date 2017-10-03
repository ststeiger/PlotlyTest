
namespace Http
{


    export class URL
    {

        // Read a page's GET URL variables and return them as an associative array.
        public static Parameters: string[] = (function (): string[]
        {
            var vars: string[] = [], hash: string[];
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');

            for (var i = 0; i < hashes.length; i++)
            {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }

            return vars;
        })()
        ;

        public static contains(key: string): boolean
        {
            if (!key) return false;

            var i: number = this.Parameters.length;
            while (i--)
            {
                if (this.Parameters[i] !== null && key !== null && (this.Parameters[i].toLowerCase() === key.toLowerCase()))
                    return true;
            }

            return false;
        }

    } // End static class URL


    export abstract class RequestBase
    {
        public url: string;
        public data: any;
        public Complete: boolean;

        protected m_Async;
        protected m_SuccessCallbacks: (() => any)[];
        protected m_FailureCallbacks: (() => any)[];
        protected m_CompleteCallbacks: (() => any)[];

        protected XMLHttpFactories: any[];


        constructor(pURL: string, pData: any)
        {
            this.url = pURL;
            this.data = pData;
            this.m_Async = true;

            this.XMLHttpFactories = [
                function () { return new XMLHttpRequest() },
                // function () { return new ActiveXObject("Msxml2.XMLHTTP") },
                // function () { return new ActiveXObject("Msxml3.XMLHTTP") },
                // function () { return new ActiveXObject("Microsoft.XMLHTTP") }
            ];

            this.m_SuccessCallbacks = [];
            this.m_FailureCallbacks = [];
            this.m_CompleteCallbacks = [];
        }


        protected failureDefault(r)
        {
            console.log("failure");
            console.log(r);
            var msg: string = "Error " + r.status + " (" + r.statusText + "): \n\n";
            msg += "URL: \n" + r.responseURL + "\n\n";
            msg += r.responseText;
            alert(msg);
        }

        protected successCallback()
        {
            this.Complete = true;

            for (var i: number = 0; i < this.m_SuccessCallbacks.length; ++i)
            {
                this.m_SuccessCallbacks[i].apply(this, arguments);
            }
        }

        protected failureCallback()
        {
            this.Complete = true;

            if (this.m_FailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);

            for (var i: number = 0; i < this.m_FailureCallbacks.length; ++i)
            {
                this.m_FailureCallbacks[i].apply(this, arguments);
            }
        }

        protected alwaysCallback()
        {
            this.Complete = true;

            for (var i: number = 0; i < this.m_CompleteCallbacks.length; ++i)
            {
                this.m_CompleteCallbacks[i].apply(this, arguments);
            }
        }


        public success(cb: () => any)
        {
            this.m_SuccessCallbacks.push(cb);
            return this;
        }

        public failure(cb: () => any)
        {
            this.m_FailureCallbacks.push(cb);
            return this;
        }

        public always(cb: () => any)
        {
            this.m_CompleteCallbacks.push(cb);
            return this;
        }

        public async(b: boolean)
        {
            this.m_Async = b;
            return this;
        }


        protected urlEncode(pd)
        {
            if (typeof pd == 'string' || pd instanceof String)
                return pd; // encodeURI(pd); // Might be stringified JSON

            var k, sb = [];
            for (k in pd)
                sb.push(encodeURIComponent(k) + "=" + encodeURIComponent(pd[k]));

            return ("&" + sb.join("&"));
        }


        protected createXMLHTTPObject(): XMLHttpRequest
        {
            var xmlhttp = false;
            for (var i = 0; i < this.XMLHttpFactories.length; i++)
            {
                try
                {
                    xmlhttp = this.XMLHttpFactories[i]();
                }
                catch (e)
                {
                    continue;
                }
                break;
            } // Next i

            return <XMLHttpRequest><any>xmlhttp;
        } // End Function createXMLHTTPObject


        protected sendRequest(url:string, additionalUrlParameters, onSuccess, onError, onDone, postData, contentType:string, method:string)
        {
            url += ((url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
            if (additionalUrlParameters !== null)
                url += additionalUrlParameters;

            if (postData)
            {
                if (!method)
                    method = "POST";

                if (!contentType)
                    contentType = 'application/x-www-form-urlencoded';

                if (contentType.indexOf("application/json") != -1)
                {
                    if (!(typeof postData == 'string' || postData instanceof String))
                        postData = JSON.stringify(postData);
                }

                if (contentType.indexOf("application/x-www-form-urlencoded") != -1)
                    postData = this.urlEncode(postData);
            }
            else if (!method)
                method = "GET";

            var req = this.createXMLHTTPObject();
            if (!req) return;

            // req.onerror = function (e)
            // {
            //     console.log(e);
            //     console.log("Error Status: " + e.target.status);
            // };

            req.open(method, url, true);
            // req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');
            req.setRequestHeader('cache-control', 'no-cache');

            if (postData)
                req.setRequestHeader('Content-type', contentType);

            req.onreadystatechange = function ()
            {
                if (req.readyState != 4) return;

                if (!(req.status != 200 && req.status != 304 && req.status != 0))
                {
                    if (onSuccess)
                    {
                        
                        if(contentType.toLowerCase().indexOf("application/json") !== -1)
                            onSuccess(JSON.parse(req.responseText));
                        else
                            onSuccess(req.responseText);
                        
                    }
                        
                }

                if (req.status != 200 && req.status != 304 && req.status != 0)
                {
                    if (onError)
                        onError(req);
                    else
                    {
                        alert('HTTP error ' + req.status);
                        // return;
                    }
                }

                if (onDone)
                    onDone(req);
            }

            if (req.readyState == 4) return;
            req.send(postData);
        }

        public abstract send(): void;
    } // End Class RequestBase


    export class Get extends RequestBase
    {
        public contentType: string = "application/urlencode";
        public method: string = "GET";

        constructor(pURL: string, pData: any)
        {
            super(pURL, pData);
        }


        public send(): void
        {
            this.Complete = false;
            this.sendRequest(this.url, this.urlEncode(this.data), this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), null, this.contentType, this.method)
        }

    } // End Class Get


    export class Json extends RequestBase
    {
        public contentType: string = "application/json; charset=UTF-8";
        public method: string = "POST";

        constructor(pURL: string, pData: any)
        {
            super(pURL, pData);
        }

        public send(): void
        {
            this.Complete = false;
            this.sendRequest(this.url, "", this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), this.data, this.contentType, this.method)
        }

    } // End Class Json 


    export class Post extends RequestBase
    {
        public contentType: string = "application/x-www-form-urlencoded";
        public method: string = "POST";

        constructor(pURL: string, pData: any)
        {
            super(pURL, pData);
        }

        public send(): void
        {
            this.Complete = false;
            this.sendRequest(this.url, "", this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), this.data, this.contentType, this.method)
        }

    } // End Class Post


    export class RequestChain
    {
        private Requests: Http.RequestBase[];
        private m_OnComplete: (() => any)[];

        constructor()
        {
            this.Requests = [];
            this.m_OnComplete = [];
        }


        private internalComplete()
        {
            var allComplete: boolean = true;

            for (var i: number = 0; i < this.Requests.length; ++i)
            {
                if (!this.Requests[i].Complete)
                {
                    allComplete = false;
                    break;
                }
            }

            if (allComplete)
            {
                // console.log("all complete");
                for (var i: number = 0; i < this.m_OnComplete.length; ++i)
                {
                    this.m_OnComplete[i]();
                }
            }

        } // End Function internalComplete


        public add(...args)
        {
            var me = this;

            for (var i = 0; i < arguments.length; ++i)
            {
                arguments[i].always(
                    function ()
                    {
                        me.internalComplete();
                    }
                );

                this.Requests.push(arguments[i])
            } // Next i

            return this;
        }


        public addRange(req: Http.Get[])
        {
            var me = this;

            for (var i = 0; i < req.length; ++i)
            {
                req[i].always(function ()
                {
                    me.internalComplete();
                }
                );

                this.Requests.push(req[i])
            } // Next i

            return this;
        }


        public whenDone(cb: () => any)
        {
            this.m_OnComplete.push(cb);
            return this;
        }

        public process()
        {
            for (var i: number = 0; i < this.Requests.length; ++i)
            {
                this.Requests[i].send();
            }
        }

    } // End Class RequestChain


} // End Namespace

// new Http.Json("foo", null).send()
// new Http.Get("foo", null).send()
// new Http.Post("foo", null).send()

// new Http.RequestChain().add(req1).add(req2)
//  .whenDone(
//      function ()
//      {
//          alert("foo!");
//          console.log(this);
//      }.bind(this)
//  ).process();
