tutorialsApp.controller('testController', function ($scope, $http, currentTab) {

    $scope.LoadTest = function () {
        console.log("in LoadTest");
        var divDoc = angular.element(document.getElementById('Test'));
        var divWrap = angular.element(document.createElement('div')).addClass('test');
        var pQuest = angular.element(document.createElement('p')).addClass('question');
        var pAnswer = angular.element(document.createElement('p')).addClass('answer');
        var respData;
        $http({ method: 'GET', url: '/Test/GetQuestion', params: { id: currentTab.get() } }).
            then(function succes(response) {
                respData = response.data;

                for (var i = 0; i < respData.length; i++) {
                    divWrap = angular.element(document.createElement('div')).addClass('test');
                    pQuest = angular.element(document.createElement('p')).addClass('question').html(respData[i]['Text']);
                    pAnswer = angular.element(document.createElement('p')).addClass('answer');
                    $scope.CreateAnswer(pAnswer, respData[i]['Id']);
                    divWrap.append(pQuest).append(pAnswer);
                    divDoc.append(divWrap);
                }
            })
    }

    $scope.rightAnswer = [];
    $scope.CreateAnswer = function (pAnswer, id) {
        console.log("in CreateTest");
        var radio = angular.element(document.createElement('input')).attr('type', 'radio').attr('name', id);
        var answer = angular.element(document.createElement('span')).addClass('answer');
        var respData;

        return $http({ method: 'GET', url: '/Test/GetTest', params: { id: id } }).
            then(function succes(response) {
                respData = response.data;
                for (var i = 0; i < respData.length; i++) {
                    if (respData[i]['Checked'] == "True") {
                        $scope.rightAnswer.push(respData[i]['Id']);
                        console.log(respData[i]['Text']);
                    }

                    var answerWrap = angular.element(document.createElement('p')).addClass('answerP');
                    radio = angular.element(document.createElement('input')).attr('type', 'radio').attr('name', id).attr('value', respData[i]['Id']);
                    answer = angular.element(document.createElement('span')).addClass('answerText').html(respData[i]['Text']);
                    answerWrap.append(radio).append(answer);
                    pAnswer.append(answerWrap);
                }
        });
    }

    $scope.SumUp = function () {
        var resDiv = angular.element(document.getElementById('Result'));
        var score = 0;
        var form = document.forms[0];
        for (var i = 0; i < form.length; i++) {
            var input = angular.element(form[i]);

            if (input.attr('type') == 'radio') {
                if (input.prop('checked') == true) {
                    for (var j = 0; j < $scope.rightAnswer.length;j++) {
                        if ($scope.rightAnswer[j] == input.val()) {
                            score++;
                        }
                    }
                }
            }
        }
        resDiv.text(score);
    }
});