function showResults(results, sortType) {
    let container = document.getElementById("resultsContainer");
    let HTML = "";
    let counter = 0;
    let products = [];
    results.forEach(r => {
        r.data.forEach(product => {
            products.push(product);
            counter += 1;
        });
    });
    let sorted = [];
    if (sortType == "ascending") {
        sorted = Array.from(products);
        sorted.sort(function (a, b) {
            if (a.cost > b.cost) {
                return 1;
            }
            if (a.cost < b.cost) {
                return -1;
            }
            return 0;
        });
    }
    else if (sortType == "descending") {
        sorted = Array.from(products);
        sorted.sort(function (a, b) {
            if (a.cost < b.cost) {
                return 1;
            }
            if (a.cost > b.cost) {
                return -1;
            }
            return 0;
        });
    }
    else {
        sorted = Array.from(products);
    }
    sorted.forEach(product => {
        HTML += '<div class="col-md-4" > \
                <a style="min-height:4em;" class="card mb-4 box-shadow text-decoration-none text-dark" href="/Products/Details/'+ product.id + '"> \
                    <img class="mx-auto my-4" src="'+ product.image_url + '" width="150">\
                    <div class="card-body">\
                        <p class="card-text" style="min-height:4.5em;">'+ product.name + '</p>\
                        <div class="d-flex flex-row justify-content-between align-flex-end">\
                            <h3>'+ product.cost + ' ₴</h3>\
                            <button class="btn d-flex align-items-center text-success">\
                                <svg class="bi" width="30" height="30"><use xlink:href="#cart"></use></svg>\
                            </button>\
                        </div>\
                    </div>\
                </a>\
            </div>';
    });
    container.innerHTML = HTML;

    let count = document.getElementById("count");
    count.innerHTML = "Найдено " + counter + " товаров";
}

function changeSort() {
    let sort = document.getElementById("sortType").value;
    showResults(results, sort);
}

showResults(results, "relevant");