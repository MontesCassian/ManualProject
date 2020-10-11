tutorialsApp.controller('editorController', function ($scope, $http) {

    $scope.PostForm = function (answer, dataIn) {
        var responseData;
        $http({ method: 'POST', url: '/Editor/AddTutorialObject', data: answer }).
            then(function succes(response) {
                responseData = response.data;
                var p = angular.element(document.createElement("p"));
                var div = angular.element(document.getElementById('result'));
                p.html("Recieve: " + response + "<br>" + answer);
                div.append(p);
            });
    }

    $scope.GetBdList = function () {

        $http({ method: 'GET', url: '/Editor/GetBd' }).
            then(function succes(response) {
                $scope.Bd = response.data;
            });
    }
})