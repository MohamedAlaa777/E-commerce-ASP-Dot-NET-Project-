var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7281/api/Setting", {}, "json",
            function (data) {
                console.log(data);
                $("#lnkFacebook").attr("href", data.facebookLink);
                $("#lnkGoogle").attr("href", data.googleLink);
                $("#lnkTwitter").attr("href", data.twitterLink);
                $("#lnkInstagram").attr("href", data.instgramLink);
                
            }, function () { });
    }
}

ClsSettings.GetAll();