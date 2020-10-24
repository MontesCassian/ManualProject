tutorialsApp.controller('sidebarController', function($scope, $http, currentTab){
    $scope.createMenu = function(){
        console.log("Entire");
        var ul;
        var li;
        var a;
        var responseData;
        $http({method: 'GET', url: '/Tutorials/GetMenuList'}).
            then(function success(response){
                var httpResponse = response;
            
        console.log(httpResponse);
                responseData = httpResponse.data;

        for(var i=0; i<responseData.length; i++){
            ul = angular.element(document.getElementById('menuList'));
            li = angular.element(document.createElement("li"));
            a = angular.element(document.createElement("a"));
            a.attr("href", "javascript:void(0)");
            a.val(responseData[i]["Id"]);
            console.log(responseData[i]["Id"]);
            ul.append(li);
            li.append(a);
            a.on('click', function(e) { $scope.showContent(e.target.value)});
            a.text(responseData[i]["MenuTitle"]);
        }
    });
    }
    $scope.showContent = function(id){
        console.log("In Show Content Method - " + id);
        var cont = angular.element(document.getElementById('content'));
        var title = angular.element(document.getElementById('contTitle'));
        var loading = angular.element(document.getElementById('loading'));
        var responseData;
        $http({ method: 'GET', url: '/Tutorials/GetContent', params: {id: id }}).
            then(function success(response) {
                    responseData = response.data;
                    title.text(responseData[0]["Title"]);
                    cont.text(responseData[0]["Text"]);
                    currentTab.set(responseData[0]["Id"]);
            }
        );
    }
    $scope.logEntire = function(){
        console.log("In Controller Method");
    }
});
