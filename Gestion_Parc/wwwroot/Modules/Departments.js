
$(document).ready(function () {
    $('#TableDepartments').DataTable({
        "autowidth": false,
        "responsive": true
    });
   
    $('#').DataTable({
        "responsive": true
    });
});

Edit = (id, name) => {
    document.getElementById("title").innerHTML = "Modifier Department";
    document.getElementById("btnSave").value = "Modifier";
    document.getElementById("departmentId").value = id;
    document.getElementById("departmentName").value = name;
}

Reset = () => {
    document.getElementById("title").innerHTML = "Ajouter Nouveau Department";
    document.getElementById("btnSave").value = "Ajouter";
    document.getElementById("departmentId").value = "";
    document.getElementById("departmentName").value = "";
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

            window.location.href = `/Department/Delete?Id=${id}`;

        }
    })
}





