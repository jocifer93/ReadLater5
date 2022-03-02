setTimeout(showWidget, 1000);

function showWidget() {
    getRecentBookmarks();
}

function getRecentBookmarks() {
    var number = getAttribute("data-number");
    var username = getAttribute("data-username");

    $.get("/api/WidgetApi/GetMostRecentBookmarks", { number: number, username: username }, function (data) {
        appendWidget(number, username, data);
    });
}

function getAttribute(attributeName) {
    return $("#recent-bookmarks-widget").attr(attributeName);
}

function appendWidget(number, username, bookmarks) {

    var listGroupItems = '';

    bookmarks.forEach(function (bookmark) {
        listGroupItems += `<li class="list-group-item">${bookmark.url}</li>`
    });

    var result = `<div class="card" style="position: fixed;right: 1em;bottom: 1em;z-index: 999;">
                    <div class="card-header">
                        ${username} ${number} recent bookmarks
                    </div>
                    <ul class="list-group list-group-flush">
                        ${listGroupItems}
                    </ul>
                </div>`;

    $("body").append(result);
}