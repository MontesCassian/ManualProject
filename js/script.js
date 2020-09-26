/////////////////////////////////////////////////////
//CREATE MENU WITH PAGE LOADING
/////////////////////////////////////////////////////
$(window).ready(function(){
    console.log("Entire");
    var http = new XMLHttpRequest();
    var ul = document.getElementById('menuList');
    var li = document.createElement("li");
    var a = document.createElement("a");
    a.href = "#";
    if(http){
        console.log("if #1");
        http.open('get', 'json/tutorials.json');
        console.log(http);
        http.onreadystatechange = function (){
        if(http.readyState == 4){
            console.log("if #2");
            jsonData = JSON.parse(http.responseText);

            for(var i=0; i<jsonData.length; i++){
                ul = document.getElementById('menuList');
                li = document.createElement("li");
                a = document.createElement("a");
                a.href = "#";
                console.log(jsonData[i]["name"]);
                ul.insertAdjacentElement("beforeEnd", li);
                li.insertAdjacentElement("afterBegin", a);
                a.setAttribute("onclick", "showTutorial('"+jsonData[i]["name"]+"')" );
                a.innerText = jsonData[i]["menuTitle"] + "LOVE";
            }
        }
    }
    http.send();
}
    var a 
})
///////////////////////////////////////////////////////////////
// LOAD CONTENT BY CLICKING ON <a> THAT LOCATED ON SIDEBAR
//////////////////////////////////////////////////////////////
function showTutorial(name){
    console.log("show");
    var cont = document.getElementById('content');
    var title = document.getElementById('contTitle');
    var loading = document.getElementById('loading');
    var jsonData;
    var http = new XMLHttpRequest();

    if(http){
        http.open('get', 'json/tutorials.json');
        http.onreadystatechange = function (){
            if(http.readyState == 4){
                console.log(http);
                jsonData = JSON.parse(http.responseText);             

                for(var i=0; i<jsonData.length; i++){
                    if(jsonData[i]["name"] == name){
                        title.innerText = jsonData[i]["title"];
                        cont.innerHTML = jsonData[i]["text"];
                        console.log(jsonData[i]["title"]);
                    }
                }
            }
        }
        http.send(null);
    }
}
