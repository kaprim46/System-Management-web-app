$(document).ready(function () {
    $('#TableRoles').DataTable();
});

Edit = (id, name) => {
    document.getElementById("title").innerHTML = "Modifier Service";
    document.getElementById("btnSave").value = "Modifier";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Reset = () => {
    document.getElementById("title").innerHTML = "Ajouter Nouveau Service";
    document.getElementById("btnSave").value = "Ajouter";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
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

            window.location.href = `/Role/DeleteRole?Id=${id}`;

        }
    })
}



