var Utils = {};
Utils.dotNetDate = function dotNetDate (date) {
  return new Date(parseInt(date.substr(6)));
};