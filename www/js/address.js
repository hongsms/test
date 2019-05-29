function ProvinceChanged(provinces, citys, towns, WebSiteID) {
    $("#" + citys)[0].length = 1;
    $("#" + towns)[0].length = 1;
    if ($("#" + provinces)[0].selectedIndex == 0 || $("#" + provinces).val() == "0") {
        return;
    }
    getSubRegion($("#" + provinces).val(), citys, WebSiteID);
}
function CityChanged(provinces, citys, towns, WebSiteID) {
    $("#" + towns)[0].length = 1;
    if ($("#" + citys)[0].selectedIndex == 0 || $("#" + citys).val() == "0") {
        return;
    }
    getSubRegion($("#" + citys).val(), towns, WebSiteID)
}
function TownsChanged(provinces, citys, towns, WebSiteID) {
    if ($("#" + towns)[0].selectedIndex == 0 || $("#" + towns).val() == "0") {
        return;
    }
}
function getSubRegion(parentid, toobj, WebSiteID) {
    $.ajax({
        url: "/Web/Ajax/Ajax.ashx",
        type: "POST",
        cache: true,
        dataType: "text",
        data: { action: "GetSubRegion", parentid: parentid, WebSiteID: WebSiteID },
        success: function (ReturnData) {
            if (ReturnData != "no") {
                var _arr = eval("(" + ReturnData + ")");
                for (var i = 0; i < _arr.length - 1; i += 2) {
                    with ($("#" + toobj).get(0)) {
                        options[options.length] = new Option(_arr[i], _arr[i + 1]);
                    }
                }
            }
        }
    });
}