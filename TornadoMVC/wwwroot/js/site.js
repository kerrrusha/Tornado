function getWidthVal(int_width) {
	return int_width.toString() + "px"
}
function getWidth(widthVal) {
	return parseFloat(widthVal.substr(0,widthVal.search("px")));
}

function calculateElementsWidth() {
	var intViewportWidth = window.innerWidth;

	var leftPanel = document.getElementById("navLeftPanel");
	var rightPanel = document.getElementById("navRightPanel");

	var rightPanelMarginRight = getWidth(window.getComputedStyle(leftPanel,null).getPropertyValue("margin-right"));
	var panelWidth = leftPanel.offsetWidth + rightPanel.offsetWidth + rightPanelMarginRight;
	var margin = 32;

	//document.getElementById("navRightPanel").style.width = getWidthVal(200);
	document.getElementById("searchForm").style.width = getWidthVal(getWidth(document.getElementById("searchForm").style.width) + rightPanelMarginRight - margin);
	document.getElementById("searchInput").style.width = getWidthVal(getWidth(document.getElementById("searchForm").style.width) + rightPanelMarginRight - margin);
}

// document.addEventListener("DOMContentLoaded", calculateElementsWidth());
// $(window).resize(calculateElementsWidth);

var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
  return new bootstrap.Popover(popoverTriggerEl)
})

function hexToString(hexEncoded) {
    let REG_HEX = /&#x([a-fA-F0-9]+);/g;

    let decoded = hexEncoded.replace(REG_HEX, function (match, group1) {
        let num = parseInt(group1, 16); //=> 39
        return String.fromCharCode(num); //=> '
    });

    return decoded;
}

class Product {
    constructor(id, name, category_id, cost, old_cost, image_url, remains, description, creating_date, code) {
        this.id = id;
        this.name = name;
        this.category_id = category_id;
        this.cost = cost;
        this.old_cost = old_cost;
        this.image_url = image_url;
        this.remains = remains;
        this.description = description;
        this.creating_date = creating_date;
        this.code = code;
    }
}

class Dataset {
    constructor(name, data) {
        this.name = name;
        this.data = data;
    }
};

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
