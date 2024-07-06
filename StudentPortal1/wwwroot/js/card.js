document.addEventListener('DOMContentLoaded', () => {

    var empdiv = document.createElement('div');
    empdiv.classList.add('employee-list');

    document.getElementById('submit').addEventListener("click", addElement);

    function addElement() {
        var name = document.getElementById('Name').value;
        var designation = document.getElementById('Designation').value;
        var role = document.getElementById('EmailId').value;
        var gender = document.querySelector('input[name="Gender"]:checked').value;
        var source = gender === 'male' ? 'Images/cardjs/rr.jpg' : 'Images/cardjs/female.jpg';

        var employeedetails = `sss
<style>
    .card {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        border: 1px solid #ddd;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        margin: 20px 0;
        background-color: #fff;
        font-family: Arial, sans-serif;
    }

    .card img {
        max-width: 50px; /* Adjust the max-width as needed */
        height: auto;
        object-fit: cover;
        border-radius: 8px 0 0 8px;
    }

    .emp-details {
        flex: 1;
        padding: 20px;
       
    }

    .emp-name {
        font-size: 20px;
        font-weight: bold;
        color: #333;
        margin-bottom: 5px;
    }

    .emp-designation {
        font-size: 16px;
        color: #777;
        margin-bottom: 10px;
    }

    .emp-role {
        font-size: 14px;
        color: #555;
        margin-bottom: 15px;
    }

    .div-remove-card {
        padding-right: 15px;
        display: flex;
        align-items: center;
    }

    .div-remove-card button {
        background-color: transparent;
        border: none;
        cursor: pointer;
        padding: 0;
    }

    .div-remove-card button img {
        width: 50px;
        height: 50px;
    }
</style>




      
      <div class="card">
              <img src="${source}" >
              <div class="emp-details">
                  <div class="emp-name">${name}</div>
                  <div class="emp-designation"><i>${designation}</i></div>
                  <div class="emp-role">${EmailId}</div>
              </div>
              <div class="div-remove-card">
                  <button>
                      <img src="delete.png" height="50" width="50" alt="Delete Button">
                  </button>
              </div>
          </div>`;

        empdiv.insertAdjacentHTML('beforeend', employeedetails);
        document.querySelector('.container').appendChild(empdiv);


    }


    // Add the employeedetails to the DOM (for example purposes, adding it to the body)
    //document.body.innerHTML += employeedetails;

});
