var WCMS = window.WCMS || {};

WCMS.Social = {
    bindCommentHover: function (selections) {
        selections.hover(
                function () { // Enter
                    var post = $(this).find("a.commentDeleteButton");
                    post.css("background-position", "0 0");
                    post.css("visibility", "visible");
                },
                function () { // Leave
                    $(this).find("a.commentDeleteButton").css("visibility", "hidden");
                }
            );
    },

    bindDeleteButtonHover: function (selections) {
        selections.hover(
                function () { // Enter
                    $(this).css("background-position", "0 -32px");
                },
                function () { // Leave
                    $(this).css("background-position", "0 0");
                }
            );
    },

    bindPostHover: function (selections) {
        selections.hover(
                function () { // Enter
                    var post = $(this).find("a.postDeleteButton");
                    post.css("background-position", "0 0");
                    post.css("visibility", "visible");
                },
                function () { // Leave
                    $(this).find("a.postDeleteButton").css("visibility", "hidden");
                }
            );
    },

    bindPostDeleteClick: function (selections) {
        selections.click(function (event) {
            var wallData = $(event.target).parents("div.panelWallPost").data("post");
            var fxData = $("body").data("fx");

            if (fxData.userId > 0 && confirm("Are you sure you want to delete this post?")) {
                $.ajax({
                    type: "POST",
                    url: "/Content/Parts/Social/WebService.asmx/DeleteWallEntry",
                    data: JSON.stringify({
                        "id": wallData.id
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d) {
                            $("#panelWallUpdates div#wallPost" + wallData.id).remove();
                        }
                    },
                    error: function (request, status, error) {
                        alert(request.responseText);
                    }
                });
            }
        });
    },

    bindCommentDeleteClick: function (selections) {
        selections.click(function (event) {
            var commentItem = $(event.target).parents("div.commentItem");
            var commentData = commentItem.data("comment");
            var fxData = $("body").data("fx");

            if (fxData.userId > 0 && confirm("Are you sure you want to delete this comment?")) {
                $.ajax({
                    type: "POST",
                    url: "/Content/Parts/Common/FxService.asmx/DeleteComment",
                    data: JSON.stringify({
                        "id": commentData.id
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d) {
                            commentItem.remove();
                        }
                    },
                    error: function (request, status, error) {
                        alert(request.responseText);
                    }
                });
            }
        });
    },

    bindNewCommentKeypress: function (selections) {
        selections.keyup(function (event) {
            if (event.keyCode == 8) {
                WCMS.Social.autoSizeTextbox($(event.target), 14, 14);
            }
        });

        selections.each(function (index) {
            var textbox = $(this);
            var commentButton = $(this).next("input[type='button']");
            $(commentButton).click(function () {
                WCMS.Social.postCommentAjax(textbox);
            });
        });

        selections.keypress(function (event) {
            var commentTextbox = $(event.target);

            if (event.which == 13 && !event.shiftKey) {
                WCMS.Social.postCommentAjax(commentTextbox);
            }
            else if (event.which == 13 || event.which == 8) {
                // auto shrink / grow textbox
                WCMS.Social.autoSizeTextbox(commentTextbox, 14, 14);
            }
        });
    },

    postCommentAjax: function (commentTextbox) {
        var commentText = $(commentTextbox).val();
        var wallData = $(commentTextbox).parents("div.panelWallPost").data("post");
        var fxData = $("body").data("fx");

        if (fxData.userId > 0 && !commentText.isEmpty()) {
            $.ajax({
                type: "POST",
                url: "/Content/Parts/Common/FxService.asmx/PostComment",
                data: JSON.stringify({
                    "commentText": commentText,
                    "userId": fxData.userId,
                    "targetObjectId": wallData.oid,
                    "targetRecordId": wallData.rid
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var commentsPanel = $(commentTextbox).parents("div.panelComments").children("div.allComments");

                    commentsPanel.append(data.d);

                    var commentItem = $(commentsPanel).find("div.commentItem");
                    var commentDeleteButton = commentItem.find("a.commentDeleteButton");

                    // BIND EVENTS
                    WCMS.Social.bindCommentHover(commentItem);
                    WCMS.Social.bindDeleteButtonHover(commentDeleteButton);
                    WCMS.Social.bindCommentDeleteClick(commentDeleteButton);

                    $(commentTextbox).val("");
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        }
        else {
            $(commentTextbox).val("");
        }
    },

    bindCommentLinkClick: function (selections) {
        selections.click(function () {
            var panel = $(this).parent().siblings("div.panelComments");
            if ($(panel).css("display").toLowerCase().indexOf("none") > -1) {
                $(panel).css("display", "block");
            }
        });
    },

    bindNewPostFocus: function () {
        var newPostPanel = $("div#panelNewWallPost textarea.newPostText");
        if (newPostPanel.length > 0) {
            newPostPanel.focus(function (event) {
                var newPostControls = $("div#panelNewWallPost div.uiPostControlContainer");
                if (newPostControls.css("display").toLowerCase().indexOf("block") <= -1) {
                    $(this).css("height", "48px");
                    newPostControls.css("display", "block");
                }
            });

            newPostPanel.keypress(function (event) {
                if (event.which == 13) {
                    WCMS.Social.autoSizeTextbox($(event.target), 16, 48);
                }
            });

            newPostPanel.keyup(function (event) {
                if (event.keyCode == 8) {
                    WCMS.Social.autoSizeTextbox($(event.target), 16, 48);
                }
            });
        }
    },

    execDisplayCommentPanel: function (selections) {
        selections.each(function (index) {
            if ($(this).children("div.allComments").children().length > 0) {
                $(this).css("display", "block");
            }
        });
    },

    autoSizeTextbox: function (textbox, lineHeight, minHeight) {
        var lineCount = $(textbox).val().split(/\r?\n|\r/).length;
        var height = lineHeight * lineCount + 15;
        $(textbox).css("height", (height >= minHeight ? height : minHeight) + "px");
    }
};

WCMS.Social.bindNewPostFocus();

WCMS.Social.bindPostHover($("#panelWallUpdates div.panelWallPost"));
WCMS.Social.bindDeleteButtonHover($("a.postDeleteButton, a.commentDeleteButton"));
WCMS.Social.bindPostDeleteClick($("#panelWallUpdates div.panelWallPost a.postDeleteButton"));

WCMS.Social.bindCommentHover($("div.commentItem"));
WCMS.Social.bindCommentDeleteClick($("div.commentItem a.commentDeleteButton"));
WCMS.Social.bindCommentLinkClick($("a.commentAction"));
WCMS.Social.bindNewCommentKeypress($("div.panelComments div.newComment textarea"));

WCMS.Social.execDisplayCommentPanel($("div.panelComments"));