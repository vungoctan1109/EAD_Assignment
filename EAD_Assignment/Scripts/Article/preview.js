$("#previewSource").click(function () {
    $(".result-link").empty();
    var url = $("input[name=Link]").val();
    var linkSelector = $("input[name=LinkSelector]").val();
    $.ajax({
        type: "POST",
        url: "PreviewSource",
        data: {
            url: url,
            linkSelector: linkSelector,
        },
        success: function (data) {
            data.forEach(element => $(".result-link").append("<li>" + element + "</li>"));
        }
    });
});
$("#previewArticle").click(function () {
    $(".result-article-img").empty();
    $(".result-article-title").empty();
    $(".result-article-detail").empty();
    var titleSelector = $("input[name=TitleDetailSelector]").val();
    var descriptionSelector = $("input[name=DescriptionDetailSelector]").val();
    var imgSelector = $("input[name=ImageDetailSelector]").val();
    var detailSelector = $("input[name=ContentDetailSelector]").val();
    $.ajax({
        type: "POST",
        url: "PreviewArticle",
        data: {
            titleSelector: titleSelector,
            detailSelector: detailSelector,
            imgSelector: imgSelector,
            descriptionSelector: descriptionSelector,
        },
        success: function (data) {
            $(".result-article-img").attr("src", data.ImageUrl)
            $(".result-article-title").append(data.Title)
            $(".result-article-detail").append(data.Detail)
            console.log(data)
        }
    });
});