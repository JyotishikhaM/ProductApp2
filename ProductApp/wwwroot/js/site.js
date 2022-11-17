// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var productId = 0;
var name = "";
function DragStart(event) {
    console.log("Drag Started from product list");
    var dragId = event.target.id;
    productId = +dragId.replace("productId_", "");
    name = $('#' + dragId + ' #productName a').text();
}

function DropEvent(event) {

    console.log(productId + ' , ' + name);
    //console.log(event);
}

function AllowDrop(event) {
    event.preventDefault();
}