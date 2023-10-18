

$(document).ready(function () {
    $('#TableDBrands').DataTable({
        "autowidth": false,
        "responsive": true
    });

    $('#').DataTable({
        "responsive": true
    });
});

Edit = (id, name) => {
    document.getElementById("title").innerHTML = "Modifier Marque";
    document.getElementById("btnSave").value = "Modifier";
    document.getElementById("brandId").value = id;
    document.getElementById("brandName").value = name;
}

Reset = () => {
    document.getElementById("title").innerHTML = "Ajouter Nouveau Marque";
    document.getElementById("btnSave").value = "Ajouter";
    document.getElementById("brandId").value = "";
    document.getElementById("brandName").value = "";
}

function Delete(id) {
    Swal.fire({
        title: 'Etes-vous sûr?',
        text: "Vous ne pourrez pas revenir en arrière!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Oui, supprimez-le!'
    }).then((result) => {
        if (result.isConfirmed) {

            window.location.href = `/Brand/Delete?Id=${id}`;

        }
    })
}


function DeleteLog(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            window.location.href = `/Brand/DeleteLog?Id=${id}`;

        }
    })
}



document.getElementById("defaultOpen").click();

function openCity(evt, cityName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
