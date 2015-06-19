(function () {
	'use strict';

	var module = angular.module("mainApp");

	// this code can be used with uglify
	module.controller("alarmsController",
		[
			"$scope",
			"$log",
			"alarmsService",
			AlarmsController
		]
	);

	function AlarmsController($scope, $log, alarmsService) {
	    $log.info("alarmsController called");
	    $scope.message = "Add an alarm to elasticsearch";

	    $scope.AlarmType = "info";
	    $scope.Message = "";

	    $scope.CreateNewAlarm = function () {
	       
	        console.log("CreateNewAlarm");
	        var data =  { AlarmType: $scope.AlarmType, Message: $scope.Message };
	        alarmsService.addAlarm(data)
	    };

	   

	}
})();
