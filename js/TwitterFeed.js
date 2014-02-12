function setupTweets(mid) {

    var sf = $.ServicesFramework(mid);

    $.ajax({
        type: "GET",
        url: sf.getServiceRoot('Connect/TwitterFeed') + 'ClientFeed/TwitterSearchResult',
        beforeSend: sf.setModuleHeaders,
        success: function (data) {
            if (data.length > 0) {
                $(".ConnectTweetContainer").hide().html(data).slideDown("slow");
            }
            if (typeof (callback) != "undefined") {
                callback(data);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}