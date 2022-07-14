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

