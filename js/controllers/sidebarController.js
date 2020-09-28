tutorialsApp.controller('sidebarController', function($scope, $http){
    $scope.createMenu = function(){
        console.log("Entire");
        var ul;
        var li;
        var a;
        var responseData;
        $http({method: 'GET', url: 'json/tutorials.json'}).
            then(function success(response){
                var httpResponse = response;
            
        console.log(httpResponse);
                responseData = httpResponse.data;

        for(var i=0; i<responseData.length; i++){
            ul = angular.element(document.getElementById('menuList'));
            li = angular.element(document.createElement("li"));
            a = angular.element(document.createElement("a"));
            a.attr("href", "javascript:void(0)");
            a.val(responseData[i]["name"]);
            console.log(responseData[i]["name"]);
            ul.append(li);
            li.append(a);
            a.on('click', function(e) { $scope.showContent(e.target.value)});
            a.text(responseData[i]["menuTitle"]);
        }
    });
    }
    $scope.showContent = function(name){
        console.log("In Show Content Method - " + name);
        var cont = angular.element(document.getElementById('content'));
        var title = angular.element(document.getElementById('contTitle'));
        var loading = angular.element(document.getElementById('loading'));
        var responseData;
        $http({method: 'GET', url: 'json/tutorials.json'}).
        then(function succes(response){
            responseData = response.data;
            
            for(var i=0; i<responseData.length; i++){
                if(responseData[i]["name"]==name){
                    title.text(responseData[i]["title"]);
                    cont.text(responseData[i]["text"]);
                }
            }
        });
    }
    $scope.logEntire = function(){
        console.log("In Controller Method");
    }
});
