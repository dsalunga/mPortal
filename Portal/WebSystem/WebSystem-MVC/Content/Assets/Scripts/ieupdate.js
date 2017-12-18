objects = document.getElementsByTagName("embed");
for (var i = 0; i < objects.length; i++)
{
    objects[i].outerHTML = objects[i].outerHTML;
}
