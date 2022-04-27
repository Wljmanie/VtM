let index = 0;

function AddChronicleTenet() {
    //var chronicleTenetEntry = 
    //console.log("Jaa AddChronicleClicks");
    var tenetEntry = document.getElementById("tenetId");
    var characterEntry = document.getElementById("characterId");

    let searchResult = Search(tenetEntry.value);

    if (searchResult != null) {
        swalWithDarkButton.fire({
            html: `<span class="font-weight-bolder">${searchResult}</span>`
        });
    }
    else {
        let newOption = new Option(tenetEntry.value, tenetEntry.value);
        document.getElementById("TenetList").options[index++] = newOption;
    }

    tenetEntry.value = "";
    return true;

}

function DeleteChronicleTenet() {
    console.log("Jaa Delete Chron Click");
}


function Search(str) {
    if (str == "") return "Empty tenets are not permitted.";

    let tenetElement = document.getElementById("TenetList");

    if (tenetElement) {
        let options = tenetElement.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `The tenet #${str} was detected as a duplicate. That is not allowed.`
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-outline-dark'
    },
    timer: 3000,
    buttonsStyling: false
});