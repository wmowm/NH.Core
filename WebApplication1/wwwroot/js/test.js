var mm;
(function (global, undefined) {
    "use strict"
    var _global;
    var getImgEle = function (gaid, fn) {


        //1.ajax请求获取图片链接和点击链接
        var clickUrl = 'http://baidu.com';
        var creativeUrl = 'https://s3-ap-southeast-1.amazonaws.com/wz-nsc-offer-imgaes/0F555B56FF084C5C9B931C165661E7DF.jpg';
        /*var creativeEle = "<a href= '#' onclick='javascript:alert("hello") '  return false;><img src= '"+creativeUrl+"'/></a>"*/
        var creativeEle = '<a onclick="mm()"><img src="' + creativeUrl + '"></a>';


        fn(creativeEle);

        /*Ajax.get("http://nsc.ninesword.com/jsForSdp/getCreatives?gaid='"+gaid+"'", function (data) {
            if(null != data && '' != data) {
                data = JSON.parse(data);	//设置返回数据是json数据
                var clickUrl = data['clickUrl'];	//点击链接
                var creativeUrl = data['creativeUrl'];	//素材链接
                var creativeEle = "<a target='javascript:Ajax.get('"+clickUrl+"');' href = '"+clickUrl+"'><img src= '"+creativeUrl+"'/></a>"
                fn(creativeEle)
            }
        });*/



    };

    mm =function hideClick() {
        console.log("哈哈哈");
    }


    //method: get/post; url:请求链接， isSync：是否异步
    var Ajax = {

        get: function (url, fn) {
            // XMLHttpRequest对象用于在后台与服务器交换数据,IE浏览器:new ActiveXObject("Microsoft.XMLHttp");其他：new XMLHttpRequest();
            var xhr = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");

            xhr.open('GET', url, true)
            xhr.onreadystatechange = function () {
                //readyStatus == 4说明请求已经完成
                if (xhr.readyState == 4 && xhr.status == 200) {
                    //从服务器获得数据
                    fn.call(this, xhr.responseText);
                }
            };
            xhr.send();
        },
        // datat应为'a=a1&b=b1'这种字符串格式，在jq里如果data为对象会自动将对象转成这种字符串格式
        post: function (url, data, fn) {
            var xhr = new XMLHttpRequest();
            xhr.open("POST", url, true);
            // 添加http头，发送信息至服务器时内容编码类型
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && (xhr.status == 200 || xhr.status == 304)) {
                    fn.call(this, xhr.responseText);
                }
            };
            xhr.send(data);
        }

    };
    // 最后将插件对象暴露给全局对象
    _global = (function () { return this || (0, eval)('this'); }());
    if (typeof module !== "undefined" && module.exports) {
        module.exports = getImgEle;
    } else if (typeof define === "function" && define.amd) {
        define(function () { return getImgEle; });
    } else {
        _global.getImgEle = getImgEle;
    }
}());

mm();