
"use strict";

angular.module('app', ['ui.bootstrap']);

angular.module('app')
    .controller('baseController', ['$scope', '$http', '$uibModal', baseController])
    .controller('modalContentCtrl', ['$scope', modalContentCtrl]);

function baseController($scope, $http, $uibModal) {
    $scope.seatchDistance = function () {
        $http.get('http://localhost:7583/Service1.svc/getDistance?soruce=' + $scope.soruce + '&destination=' + $scope.destination  )
           .then(function (request) {
                $uibModal.open({
                   templateUrl: 'modalContent.html',
                    controller: 'modalInstanceCtrl',
                    resolve: {
                        distans: request.distance,
                        distansList: request.distanceList
                    }
                });
           });
        };

};


function modalContentCtrl($scope) {

}
