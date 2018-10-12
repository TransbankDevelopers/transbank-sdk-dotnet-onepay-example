function HttpUtil() {

    this.getHttpRequest = function () {
        if (window.XMLHttpRequest) { return new XMLHttpRequest(); }
        // eslint-disable-next-line no-undef
        return ActiveXObject('Microsoft.XMLHTTP');
    };

    this.sendGetRedirect = function (destination, params) {
        var keys = Object.keys(params);
        var urlParams = keys.map(function (param) {
            return encodeURIComponent(param) + '=' + encodeURIComponent(params[param]);
        }).join('&');
        window.location = destination + '?' + urlParams;
    };
}