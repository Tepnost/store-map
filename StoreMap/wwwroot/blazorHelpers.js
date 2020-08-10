window.blazor_getElementPosition = function (id) {
    const element = document.getElementById(id);
    const rect = element.getBoundingClientRect();
    return {
        top: Math.round(rect.top),
        bottom: Math.round(rect.bottom),
        left: Math.round(rect.left),
        right: Math.round(rect.right)
    };
}
