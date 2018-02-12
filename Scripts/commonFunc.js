var commonFunc = {
    DownloadExcel: function (actionName, data) {
        //eg:data:{Data:[{A:0,IgnoreField0:0}],Header:["Header0","Header1"],IgnoreFieldList:["IgnoreField0","IgnoreField1"]}
        window.location.href = "/Api/Excel/" + actionName + "?data=" + encodeURIComponent(JSON.stringify(data));
    },
    Ajax: function (arg, scope) {
        function httpSuccess(result, successFunc, errorFunc) {
            switch (result.Status) {
                case -1:
                case "-1":
                    {
                        //login func
                        break;
                    }
                case 0:
                case "0":
                    {
                        layer.alert(result.Msg);
                        if (errorFunc !== undefined) {
                            errorFunc();
                        }
                        break;
                    }
                case 1:
                case "1":
                    {
                        if (successFunc === 1) {
                            layer.msg("操作成功", function () { window.location.reload(); });
                            return;
                        }

                        if (successFunc === undefined) {
                            layer.msg("操作成功");
                            return;
                        }
                        successFunc(result.Data);
                        break;
                    }
                default:
                    {
                        layer.alert("未知的状态码");
                        console.log(result.Status);
                    }
            }

        }
       // console.log(arg.data);
        var isJson = ($.isPlainObject(arg.data) || $.isArray(arg.data));
        arg.data = isJson ? JSON.stringify(arg.data || "") : arg.data;

        if (isJson && arg.contentType === undefined) {
            arg.contentType = arg.contentType || "application/json; charset=utf-8";
        }
        arg.type = arg.type || "post";
        var successFunc = arg.success;
        arg.success = function (result) {
            httpSuccess(result, successFunc, arg.error);
            if (scope !== undefined) {
                try {
                    scope.$apply();
                } catch (e) {
                    //do nothing
                }
            }
        };
        $.ajax(arg);
    },
    JsonIndex: function (obj, fieldName, val) {
        return obj.map(function (d) { return d[fieldName]; }).indexOf(val);
    },
    GetQuery: function (name) {
        var url = location.search;
        if (url.indexOf("?") !== -1) {
            var str = url.substr(1);
            var dataList = str.split("&");
            for (var i = 0; i < dataList.length; i++) {
                var currentData = dataList[i].split("=");
                if (currentData[0].toLowerCase() === name.toLowerCase()) {
                    return decodeURIComponent(currentData[1]);
                }
            }
        }
        return undefined;
    },
    DrawPie: function (opt) {
        var legendData = [];
        for (var i in opt.data) {
            legendData.push(opt.data[i].name);
        }
        var option = {
            title: {
                text: opt.text,//'南丁格尔玫瑰图',
                subtext: opt.desc,// '纯属虚构',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: legendData,
                x: "left"
            },
            series: [
                {
                    //  name: '访问来源',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '65%'],
                    data: opt.data,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        },
                        normal: {
                            label: {
                                show: true,
                                formatter: '{b} : {c} ({d}%)'
                            },
                            labelLine: { show: true }
                        }
                    }
                }
            ]
        };
        echarts.init(document.getElementById(opt.id)).setOption(option);
    },
    $: function (str) {
        var reg = /{\d+}/g;
        var flag = "%xa#";
        var regResult = str.match(reg);
        var numList = [];
        for (var i in regResult) {
            var oriStr = regResult[i];
            var num = oriStr.substr(1, oriStr.length - 2) * 1;
            if (num < 0) {
                throw "参数下标不可小于0";
            }
            numList.push(num);
        }
        var maxIndex = (numList.sort())[numList.length - 1];
        if (maxIndex > arguments.length - 2) {
            throw "参数输入有误";
        }
        if (regResult !== null) {
            for (var i in regResult) {
                var oriStr = regResult[i];
                var arg = oriStr.substr(1, oriStr.length - 2) * 1 + 1;
                str = str.replace(oriStr, flag + arguments[arg] + flag);
            }
        }
        return str.replace(new RegExp(flag, "g"), "");
    },
    ///filePath 应为相对路径("/file/somefile.doc")或绝对路径(必须带有"http://")
    ///本地无法使用 外网才能使用
    ViewFile: function (filePath) {
        var protocol = location.protocol;
        var origin = commonFunc.$("{0}//{1}", location.protocol, location.host);
        filePath = filePath.length > protocol.length
            ? (filePath.substr(0, protocol.length).toLowerCase() === protocol.toLowerCase()
                ? filePath
                : origin + filePath)
            : origin + filePath;
        layer.msg("正在加载..");
        $.ajax({
            url: "http://www.yozodcs.com/onlinefile",
            data:
            {
                downloadUrl: filePath,
                convertType: 0
            },
            dataType: "json",
            type: "post",
            success: function (r) {
                if (r.result === 0) {
                    window.open(r.data[0]);
                } else {
                    layer.alert(r.message);
                }
            },
            error: function (r) {
                console.log(r);
                layer.msg("加载失败");
            }
        });
    }
};