
async function main()
{
    const canvas = <HTMLCanvasElement>document.querySelector("#glCanvas");
    
    // https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement/getContext
    // const glRenderingContext: string = "webgl"; // 2d, webgl, webgl2, bitmaprenderer, experimental-webgl
    
    // Initialize the GL context
    const gl: WebGLRenderingContext = <WebGLRenderingContext>(
        canvas.getContext("webgl2")
        || canvas.getContext("webgl")
        || canvas.getContext("experimental-webgl")
    );

    const gl2: WebGLRenderingContext = <WebGLRenderingContext>canvas.getContext("webgl2");
    const gl1: WebGLRenderingContext = <WebGLRenderingContext>canvas.getContext("webgl");
    const gle: WebGLRenderingContext = <WebGLRenderingContext>canvas.getContext("experimental-webgl");
    
    if (!gl2 || !gl1 || !gle)
    {
        var messages: string[] = [];

        if (!gl2)
            messages.push("No WebGL 2.0");

        if (!gl1)
            messages.push("No WebGL 1.0");

        if (!gle)
            messages.push("No WebGL 0.1");

        console.log(messages.join('\n'));
        return;
    }


    // Only continue if WebGL is available and working
    if (!gl)
    {
        alert("Unable to initialize WebGL. Your browser or machine may not support it.");
        return;
    }


    // Set clear color to black, fully opaque
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    // Clear the color buffer with specified clear color
    gl.clear(gl.COLOR_BUFFER_BIT);
}
