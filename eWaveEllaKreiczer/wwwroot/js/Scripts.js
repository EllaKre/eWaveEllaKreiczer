
function loadMovies(moviesList) {
    if (moviesList === undefined) {
        $.ajax({
            url: 'Movies/GetAllMovies',
            method: 'GET',
            success: function (movies) {
                const container = $('#movies-container');
                container.empty();

                movies.forEach(movie => {
                    const card = `
                    <div class="col-md-4">
                        <div class="card mb-4" onclick=showCandD("${movie.title}")>
                            <img src="${movie.imageUrl}" class="card-img-top" alt="${movie.title}">
                            <div class="card-body">
                                <h5 class="card-title">${movie.title}</h5>
                                <div id="CandR_${movie.title}" style="display:none;">
                                    <p class="card-text">${movie.categories}</p>
                                    <p class="card-text"><small class="text-muted">Rating: ${movie.rating}</small></p>
                                </div>
                            </div>
                        </div>
                    </div>`;
                    container.append(card);
                });
            }
        });
    }
    else {
        const container = $('#movies-container');
        container.empty();

        moviesList.forEach(movie => {
            const card = `
                    <div class="col-md-4">
                        <div class="card mb-4" onclick=showCandD("${movie.title}")>
                            <img src="${movie.imageUrl}" class="card-img-top" alt="${movie.title}">
                            <div class="card-body">
                                <h5 class="card-title">${movie.title}</h5>
                                <div id="CandR_${movie.title}" style="display:none;">
                                    <p class="card-text">${movie.categories}</p>
                                    <p class="card-text"><small class="text-muted">Rating: ${movie.rating}</small></p>
                                </div>
                            </div>
                        </div>
                    </div>`;
            container.append(card);
        });
    }
}
function showCandD(movieDetailsId) {
    const DetailsId = document.getElementById("CandR_" +movieDetailsId);
    if (DetailsId.style.display == "none")
        DetailsId.style.display = "block";
    else
        DetailsId.style.display = "none";
}
$('#add-movie-form').submit(function (e) {
    e.preventDefault();

    const newMovie = {
        Title: $('#title').val(),
        ImageUrl: $('#imageUrl').val(),
        Categories: $('#category').val(),
        Rating: parseFloat($('#rating').val())
    };

    $.ajax({
        url: '/Movies/AddMovie',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(newMovie),
        success: function () {
            alert('Movie added successfully!');
            loadMovies();
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
});
function categoryFilter(){
    const selectedCategory = $('#categoryFilterField').val();

    $.ajax({
        url: `/Movies/GetMoviesByCategory`,
        data: { category: selectedCategory },
        method: 'GET',
        success: function (movies) {
            loadMovies(movies);
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
};
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("rating").addEventListener("input", function () {
        const ratingInput = this;
        const errorElement = document.getElementById("rating-error");

        const decimalRegex = /^(10(\.00?)?|[1-9](\.\d{1,2})?)$/;
        const isValid = decimalRegex.test(ratingInput.value);

        if (isValid) {
            errorElement.style.display = "none";
            ratingInput.classList.remove("is-invalid");
            ratingInput.classList.add("is-valid");
        } else {
            errorElement.style.display = "block";
            ratingInput.classList.remove("is-valid");
            ratingInput.classList.add("is-invalid");
        }
    });
});

