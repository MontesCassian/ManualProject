tutorialsApp.controller('editorController', function ($scope, $http, $route, $compile) {

    $scope.tutorialCount = 0;//count of recieved tutorials 
    $scope.testCount = 0;//count of recieved test
    $scope.videoCount = 0;//count of recieved video
    //Init creating DB when creating form
    $scope.CreateDb = function () {
        $http({ method: 'GET', url: '/Editor/AddTutorialObject' }).
            then(function succes(response) {
                $scope.answer = response.data[0];
                $scope.answer.Title = "New";
            });
    }
    //Post new data by editing record
    $scope.PostForm = function (answer, dataIn) {
        var responseData;
        var quotExp = new RegExp('"', 'g');
        console.log(JSON.stringify(answer.Text));
        answer.Text = answer.Text.replace(/\n/g, '<br>').replace(quotExp, '\\"').replace(/\//g, ' ');
        $http({ method: 'POST', url: '/Editor/AddTutorialObject', data: answer }).
            then(function succes(response) {
                $scope.tutorialCount++;
                responseData = response.data[0];
                var p = angular.element(document.getElementById('TutorialCount'));
                p.text("Recieved Tutorial: " + $scope.tutorialCount);
            });
    }

    //Post new video
    $scope.PostVideo = function (video, videoIn) {
        var responseData;
        video.TutorialId = $scope.answer.Id;
        $http({ method: 'POST', url: '/Editor/AddVideo', data: video }).
            then(function succes(response) {
                $scope.videoCount++;
                responseData = response.data[0];
                var p = angular.element(document.getElementById('VideoCount'));
                p.text("Recieved Video: " + $scope.videoCount);
            })
    }
    //Get list of Tutorial
    $scope.GetDbList = function () {
        $http({ method: 'GET', url: '/Editor/GetDbTitle' }).
            then(function succes(response) {
                $scope.Db = response.data;
            });
    }

    //Editing record from list by choosing
    $scope.EditDb = function (id) {
        var responseData;
        $http({ method: 'GET', url: '/Editor/GetDb', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data[0];
                $scope.answer = responseData;
            });
        //CREATING LIST OF RELATIVE VIDEO 
        $http({ method: 'GET', url: '/Videos/GetTitleByTutorialId', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data;
                var vlDiv = angular.element(document.getElementById('VideoList')).html('');
                for (var i = 0; i < responseData.length; i++) {
                    var p = angular.element(document.createElement('p')).val(responseData[i]['Id']).addClass('videoListItem').
                        text('Id: ' + responseData[i]['Id'] + ', Title: ' + responseData[i]['Title']).
                        on('click', function (e) {
                            SetVideoToEdit(e.target.value);
                        });

                    var a = angular.element(document.createElement('a')).val(responseData[i]['Id']).
                        text('Delete').
                        attr('href', 'javascript: void (0)').
                        on('click', function (e) {
                            DeleteVideo(e.target.value);
                            angular.element(e.target).parent().remove();
                        });


                    p.append(a);

                    vlDiv.append(p);
                }
            });

        $http({ method: 'GET', url: '/Test/GetQuestion', params: { id: id } }).
            then(function (response) {
                responseData = response.data;
                var qlDiv = angular.element(document.getElementById('QuestionList')).html('');
                for (var i = 0; i < responseData.length; i++) {
                    var p = angular.element(document.createElement('p')).val(responseData[i]['Id']).addClass('questionListItem').
                        text('Id: ' + responseData[i]['Id'] + ', Text: ' + responseData[i]['Text']);

                    var a = angular.element(document.createElement('a')).val(responseData[i]['Id']).
                        text('Delete').
                        attr('href', 'javascript: void (0)').
                        on('click', function (e) {
                            DeleteQuestion(e.target.value);
                            angular.element(e.target).parent().remove();
                        });


                    p.append(a);

                    qlDiv.append(p);
                }

            });
        console.log($scope.answer);
    }
    //Delete TUTORIAL record
    $scope.DeleteDb = function (id) {
        $http({ method: 'GET', url: '/Editor/DeleteDb', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data;

                var p = angular.element(document.createElement("p"));
                var div = angular.element(document.getElementById('result'));
                p.html("Deleted: " + response.data + "<br>");
                div.append(p);
            });
    }

    var DeleteQuestion = function (id) {
        $http({ method: 'GET', url: '/Editor/DeleteTest', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data;

                var p = angular.element(document.createElement("p"));
                var div = angular.element(document.getElementById('result'));
                p.html("Deleted question: " + response.data + "<br>");
                div.append(p);
            });
    }

    var SetVideoToEdit = function (id) {
        $http({ method: 'GET', url: '/Videos/GetContent', params: { id: id } }).
            then(function (response) {
                $scope.video = response.data[0];
                angular.element(document.getElementById('SaveVideoBtn')).toggleClass('collapse');
                angular.element(document.getElementById('UpdateVideoBtn')).toggleClass('collapse');
            })
    }

    $scope.EditVideo = function (video) {
        var responseData;

        $http({ method: 'POST', url: '/Editor/UpdateVideo', data: video }).
            then(function succes(response) {
                responseData = response.data[0];
                var p = angular.element(document.getElementById('TutorialCount'));
                p.text("Updated video: 1");
            });
    }

    //DELETE VIDEO RECORD
    var DeleteVideo = function (id) {
        $http({ method: 'GET', url: '/Editor/DeleteVideo', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data;

                var p = angular.element(document.createElement("p"));
                var div = angular.element(document.getElementById('result'));
                p.html("Deleted video: " + response.data + "<br>");
                div.append(p);
            });
    }
    /*
    $scope.CheckForEdit = function () {
        var responseData;
        $http({ method: 'GET', url: '/Editor/GetDb' }).
            then(function succes(response) {
                responseData = response.data;
                if (Number.isInteger(responseData) && responseData == -1) {
                    return;
                } else {
                    $scope.answer = responseData;
                }
            })
    }*/
//_________________TEST CONSTRUCTING___________
    $scope.formCount = 0;
    $scope.AddTest = function (e) {

        angular.element(e.target).addClass('collapse');//hide AddTest button

        $scope.AddPWrap = angular.element(document.createElement('p')).attr('id', 'pWrap');//
        var div = angular.element(document.getElementById('TestContainer'));
        $scope.form = angular.element(document.createElement('form')).attr('name', 'questForm');
        var quest = angular.element(document.createElement('input')).attr('name', 'question.Text').attr('type', 'text');

        var addRadio = angular.element(document.createElement('input')).attr('type', 'button').val('Add Radio').on('click', function (e) {
            $scope.SelectRadio();
        });
        
        $scope.AddPWrap.append(addRadio);//.append(addCheck);
        div.append($scope.form);
        $scope.form.append(quest).append($scope.AddPWrap)
            .append(angular.element($compile(
                '<input type="hidden" id="question.Id" name="question.Id">' +
                '<input type="hidden" id="question.TutorialId" name="question.TutorialId">'+
                '<input type="hidden" id="test.Id" name="test.Id">' +
                '<input type="hidden" id="test.QuestionId" name="test.QuestionId">' +
                '<input type="hidden" id="question.Type" name="question.Type">'
                )($scope)
            ));
        $scope.formCount++;
    }
    //-----------SELECT RADIO-----------------
    $scope.SelectRadio = function () {
        $scope.answerCount = 0;
        $scope.AddPWrap.remove();//remove AddRadio btn 

        var savBtn = angular.element(document.createElement('input')).attr('type', 'button').val('Save').on('click', function (e) {
            $scope.SaveTest(questForm);
        });
        var addRBtn = angular.element(document.createElement('input')).attr('type', 'button').val('Add').on('click', function (e) {
            $scope.CreateRadio();
        });
        $scope.SelPWrap = angular.element(document.createElement('p')).attr('id', 'pWrap').append(savBtn).append(addRBtn);
        $scope.form.append($scope.SelPWrap);
        angular.element(document.getElementById('question.Type')).val(1);
        angular.element(document.getElementById('question.TutorialId')).val($scope.answer.Id);
        $scope.CreateRadio();
    }
    //-----------CREATING RADIO-----------------
    $scope.CreateRadio = function () {

        var rBtn = angular.element(document.createElement('input')).attr('type', 'radio').attr('name', 'radio').val('answer' + $scope.answerCount);
        var answer = angular.element(document.createElement('input')).attr('name', 'answer' + $scope.answerCount).attr('type', 'text');

        var delBtn = angular.element(document.createElement('input')).attr('type', 'button').val('Del').on('click', function (e) {
            $scope.DeleteRadio(angular.element(e.target).parent().attr('id'));
        });
        var rp = angular.element(document.createElement('p')).attr('id', 'rp' + $scope.answerCount);

        rp.append(rBtn);
        rp.append(answer);
        rp.append(delBtn);
        $scope.form.append(rp);
        $scope.answerCount++;
    }

    //----------Delete radio--------------------
    $scope.DeleteRadio = function (id) {
        var rp = angular.element(document.getElementById(id)).remove();
    }

    //-----------SAVE TEST-----------------
    $scope.SaveTest = function (questForm) {
        var question = {};
        //PARSE QUESTION DATA
        for (var i = 0; i < questForm.length; i++) {
            var input = angular.element(questForm[i]);
            switch (input.attr('name')) {
                case 'question.Id': question.Id = input.val();
                    break;
                case 'question.Text': question.Text = input.val();
                    break;
                case 'question.Type': question.Type = input.val();
                    break;
                case 'question.TutorialId': question.TutorialId = $scope.answer.Id;
                    break;
               
            }
        }
        //PARSE TEST DATA
        $http({ method: 'POST', url: '/Editor/AddQuestion', data: question}).
            then(function succes(response) {
                question.Id = response.data[0].Id;
            

                var checkKey = 'radio';
                var answerKey = 'answer';
                var answerText = null;
                var test = [];
                for (var i = 0; i < questForm.length; i++) {
                    var input = angular.element(questForm[i]);

                    var answerPare = input.val().lastIndexOf(answerKey) >= 0 ? input.val() : false;

                    if (answerPare) {
                        for (var j = 0; j < questForm.length; j++) {
                            var inputNext = angular.element(questForm[i + j]);
                            if (answerPare == inputNext.attr('name')) {
                                answerText = inputNext.val();
                               break;
                            }
                        }

                        test.push({
                            Text: answerText,
                            Checked: input.prop('checked'),
                            QuestionId: question.Id
                        })
                    }        
                }

                for (var i = 0; i < test.length; i++) {
                    $http({ method: 'POST', url: '/Editor/AddTest', data: test[i] }).
                        then(function succes(response) {
                            console.log("what happened?");
                            $scope.testCount++;
                            var p = angular.element(document.getElementById('TestCount'));
                            p.text("Recieved Test: " + $scope.testCount)
                        });
                }
            });

    }
})