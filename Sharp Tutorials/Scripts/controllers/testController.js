tutorialsApp.controller('testController', function ($scope, $http) {

    $scope.LoadTest = function () {
        var divDoc = angular.element(document.getElementById('Test'));
        var divWrap = angular.element(document.createElement('div')).addClass('test');
        var pQuest = angular.element(document.createElement('p')).addClass('question');
        var pAnswer = angular.element(document.createElement('p')).addClass('answer');
        $http({ method: 'GET', url: '/Test/GetQuestion', params: {id: currentTab}).
            then(function succes(response) {
                for (var item in response.data) {
                    divWrap = angular.element(document.createElement('div')).addClass('test');
                    pQuest = angular.element(document.createElement('p')).addClass('question').html(item['Text']);
                    pAnswer = angular.element(document.createElement('p')).addClass('answer');
                    CreateAnswer(pAnswer, item.Id);
                    divWrap.append(pQuest).append(pAnswer);
                    divDoc.append(divWrap);
                }
            })
    }

    $scope.CreateAnswer = function (pAnswer, id) {
        var radio = angular.element(document.createElement('input')).attr('type', 'radio').attr('name', id);
        var answer = angular.element(document.createElement('span')).addClass('answer');

        $http({ method: 'GET', url: '/Test/GetTest', params: { id: id }).
            then(function succes(response) {
                for (var item in response.data) {
                    radio = angular.element(document.createElement('input')).attr('type', 'radio').attr('name', id);
                    answer = angular.element(document.createElement('span')).addClass('answer').html(item.Text);

                    pAnswer.append(radio).append(answer);
                }
        });
    }
});