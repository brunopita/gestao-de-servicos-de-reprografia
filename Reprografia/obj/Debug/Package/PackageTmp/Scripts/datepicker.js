﻿//http://blogs.msdn.com/b/stuartleeks/archive/2011/01/25/asp-net-mvc-3-integrating-with-the-jquery-ui-date-picker-and-adding-a-jquery-validate-date-range-validator.aspx

/// <reference path="jquery-1.4.4.js" />
/// <reference path="jquery-ui.js" />
$(document).ready(function () {
    $('.date').datepicker({ dateFormat: "dd/mm/yyyy" });
});