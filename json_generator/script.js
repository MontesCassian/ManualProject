
var testArray=[];
var result = document.getElementById("json");
function addObject(){
    var tutCont ={
        name: document.dataIn.name.value,
        menuTitle: document.dataIn.menuTitle.value,
        title: document.dataIn.title.value,
        text: document.dataIn.text.value
    }

    jsonStr= JSON.stringify(tutCont);
    result.innerHTML = jsonStr;
}

function openTest(){
    console.log("opening test panel");
    var testDiv = document.getElementById("testPanel");
    testDiv.style.display = "block";
}

var radioCount = 0;
function createRadioButton(){
    radioCount++;
    var name = document.dataIn.testName.value;
    var p = document.createElement("p");
    var variant = document.createElement("input");

    var radio = document.createElement("input");

    var panel = document.getElementById("radioPanel");

    radio.setAttribute("type", "radio");
    radio.setAttribute("name", name);

    variant.setAttribute("type", "text");
    variant.setAttribute("name", name + radioCount);

    panel.insertAdjacentElement("beforeEnd", p);
    p.insertAdjacentElement("beforeEnd", radio);
    p.insertAdjacentElement("beforeEnd", variant);
}

var testCount =0;
function addRadio(){
    var name = document.dataIn.testName.value;
    var testUnit = document.getElementById("template").cloneNode(true);
    var div = document.getElementById("testForm");
    testUnit.setAttribute("id", "test" + testCount);
    div.insertAdjacentElement("beforeend", testUnit);
}