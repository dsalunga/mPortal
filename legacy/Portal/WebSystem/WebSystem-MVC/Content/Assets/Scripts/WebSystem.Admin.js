function ShowDelete() {
    return confirm('Are sure you want to delete?');
}

function ViewTemplate(partControlId, hTemplateId) {
    window.open("/Central/Part/WebPartPreview/?PartControlTemplateId=" + partControlId, "iframePreview");
    WCMS.Dom.Get(hTemplateId).value = partControlId;
    return false;
}