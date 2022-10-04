function unicodeToChar(text) {
	return text.replace(/\\u[\dA-F]{4}/gi,
		function (match) {
			return String.fromCharCode(parseInt(match.replace(/\\u/g, ''), 16));
		});
}
function markMatches(text, query) {
	function getPositions(t, q) {
		let positions = [];
		let pos = -2;
		let _t = t;
		while (1) {
			if (positions.length === 0)
				_t = t;
			else
				_t = t.slice(positions[positions.length - 1]);
			pos = _t.search(q);
			if (pos === -1)
				break;
			if (_t !== t)
				pos += positions[positions.length - 1];
			positions.push(pos);
			positions.push(pos + query.length);
		}
		return positions;
	}

	let positionsToInsertTag = getPositions(text.toLowerCase(), query.toLowerCase());

	let i = 0;
	let tag = '';
	let extra = 0;
	positionsToInsertTag.forEach(pos => {
		if (i % 2 !== 0)
			tag = '</strong>';
		else
			tag = '<strong>';
		pos += extra;
		text = text.slice(0, pos) + tag + text.slice(pos);
		extra += tag.length;
		i += 1;
	});

	return text;
}

function clearList(list) {
	while (list.firstChild) {
		list.removeChild(list.lastChild);
	}
}
function addElemToList(list, message, controller, id, strong = false) {
	if (strong)
		message = "<strong>" + message + "</strong>";

	const li = document.createElement("li");
	li.classList = 'dropdown-item';
	li.style.overflow = "hidden";
	list.appendChild(li);

	if (typeof id !== 'undefined')
		li.innerHTML = "<a href='/" + controller + "/Details/" + id.toString() +
			"' class='text-dark text-decoration-none'>" + message + "</a>";
	else
		li.innerHTML = message;
}
function printAnswer(data, query, list) {
	if (data.value.length === 0 || typeof data.value === 'undefined') {
		const li = document.createElement("li");
		li.classList = 'dropdown-item';
		list.appendChild(li);
		li.innerHTML = "На жаль, за вашим запитом нічого не було знайдено.";
		return;
	}

	let answer = {
		"foundIn": [],
		"foundWhat": []
	}
	data.value.forEach(keyValue => {
		let foundIn = "Знайдено в \"" + keyValue.key + "\":";
		answer['foundIn'].push(foundIn);

		let matches = [];
		keyValue.value.forEach(item => {
			matches.push({ "id": item.key, "name": markMatches(item.value, query) });
		});
		answer['foundWhat'].push(matches);

		let controller = ";"
		if (keyValue.key == "Категорії")
			controller = "Categories";
		else
			controller = "Products";

		addElemToList(list, foundIn, controller, undefined, true);
		matches.forEach(elem => {
			addElemToList(list, elem.name, controller, elem.id);
		});
	});
}

function submitSearch() {
	window.location.assign("/Home/Search/?query=" + document.getElementById('searchInput').value);
}
