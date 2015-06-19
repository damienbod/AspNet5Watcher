(function () {
	'use strict';

	var module = angular.module("mainApp");

	var AlarmsController = (function () {
	    function AlarmsController(scope, log, alarmsService) {
	        scope.Vm = this;

	        this.alarmsService = alarmsService;
	        this.log = log;

	        this.log.info("alarmsController called");
	        this.message = "Add an alarm to elasticsearch";

	        this.AlarmType = "info";
	        this.Message = "";
	    }

	    AlarmsController.prototype.CreateNewAlarm = function () {
	        console.log("CreateNewAlarm");
	        var data = { AlarmType: this.AlarmType, Message: this.Message, Id:"" };
	        this.alarmsService.AddAlarm(data);
	    };

	    return AlarmsController;
	})();

    // this code can be used with uglify
	module.controller("alarmsController",
		[
			"$scope",
			"$log",
			"alarmsService",
			AlarmsController
		]
	);
})();