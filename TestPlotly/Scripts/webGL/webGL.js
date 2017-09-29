// async
function main() {
    const canvas = document.querySelector("#glCanvas");
    // https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement/getContext
    // const glRenderingContext: string = "webgl"; // 2d, webgl, webgl2, bitmaprenderer, experimental-webgl
    // Initialize the GL context
    const gl = (canvas.getContext("webgl2")
        || canvas.getContext("webgl")
        || canvas.getContext("experimental-webgl"));
    // Only continue if WebGL is available and working
    if (!gl) {
        alert("Unable to initialize WebGL. Your browser or machine may not support it.");
        return;
    }
    // Set clear color to black, fully opaque
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    // Clear the color buffer with specified clear color
    gl.clear(gl.COLOR_BUFFER_BIT);
}
/*
async function foo()
{
    await main();
}
*/ 
//# sourceMappingURL=webGL.js.map