// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let element = Document.getElementById("id");

element.AddEventListener("keyup", () => {
    //Send Request To Back End
    function run() {

        // Creating Our XMLHttpRequest object 
        let xhr = new XMLHttpRequest();
        
        // Making our connection  
        let url = `https://localhost44361/Employees/Index?InputSearch=${element.value}`;
        xhr.open("GET", url, true);

        // function execute after request is successful 
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                console.log(this.responseText);
                //Render HTML Code To Respones
            }
        }
        // Sending our request 
        xhr.send();
    }
    run();

});