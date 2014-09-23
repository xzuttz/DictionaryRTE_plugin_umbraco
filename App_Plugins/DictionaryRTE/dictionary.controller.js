angular.module("umbraco")
    .controller("ov.DictionaryDialogCtrl",
        function ($scope, $http) {
            $scope.init = function () {
                $http({ method: 'GET', url: '/umbraco/api/dictionary/GetDictionaryItems' })
                    .success(function (data) {
                        $scope.items = data;
                    });

                $http({ method: 'GET', url: '/umbraco/api/dictionary/GetLanguages' })
                    .success(function (data) {
                        $scope.languages = data;
                    });
            };

            $scope.getValue = function () {
                var dictionaryKey = angular.element("select#dictionaryKey option:selected").val();
                var lngId = angular.element("select#lngId option:selected").val();

                $http.post('/umbraco/api/dictionary/GetValue', { LngId: parseInt(lngId), DictionaryId: dictionaryKey })
                    .success(function (data) {
                        $scope.value = data.value;
                    });
            };

            $scope.insert = function() {
                $scope.submit($scope.value);
            };
        });