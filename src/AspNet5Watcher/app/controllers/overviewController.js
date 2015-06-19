(function () {
	'use strict';

	var module = angular.module("mainApp");

	// this code can be used with uglify
	module.controller("overviewController",
		[
			"$scope",
			"$log",
			"alarmsService",
			OverviewController
		]
	);

	function OverviewController($scope, $log, alarmsService) {
		$log.info("overviewController called");
		$scope.message = "Create Alarm";

	}
})();
