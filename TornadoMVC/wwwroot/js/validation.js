var id = 0
class Field {
    constructor(inputId, labelId) {
        this.input = document.getElementById(inputId)
        this.inputId = inputId

        this.label = document.getElementById(labelId)
        this.labelId = labelId

        id += 1
        this.id = id
    }

    markAsInvalid() {
        this.input.classList.add('is-invalid')
        this.label.classList.add('is-invalid')
        this.input.classList.remove('is-valid')
        this.label.classList.remove('is-valid')
    }
    markAsValid() {
        this.input.classList.remove('is-invalid')
        this.label.classList.remove('is-invalid')
        this.input.classList.add('is-valid')
        this.label.classList.add('is-valid')
    }

    isEmpty() {
        return this.input.value.length == 0
    }
    isPhone() {
        let string = this.input.value
        if (string.length == 13 && string.charAt(0) == '+') {
            string = string.substring(1)
        }
        if (!(string.length == 12 || string.length == 10)) {
            return false
        }
        for (var i = 0; i < string.length; i++) {
            let ch = string.charAt(i)
            if (isNaN(ch))
                return false
        }
        return true
    }
    isEmail() {
        try {
            let dogIndex = this.input.value.indexOf('@', 1)
            if (dogIndex == -1) {
                return false
            }

            let dotIndex = this.input.value.indexOf('.', dogIndex + 1)
            if (dotIndex == -1) {
                return false
            }
        } catch (error) {
            console.log(error.stack)
            sleepFor(5000)
        }
        let dogIndex = this.input.value.indexOf('@', 1)
        if (dogIndex == -1) {
            return false
        }

        let dotIndex = this.input.value.indexOf('.', dogIndex + 2)
        if (dotIndex == -1) {
            return false
        }

        if (this.input.value.length == dotIndex + 1) {
            return false
        }

        return true
    }
    isStrongPassword() {
        return this.input.value.length >= 6
    }
    isNumber() {
        for (var i = 0; i < this.input.value.length; i++) {
            let ch = this.input.value.charAt(i)
            if (isNaN(ch))
                return false
        }
        return true
    }

    toString() {
        return this.inputId + ': ' + this.input.value
    }
    valueIsEqual(other) {
        return this.input.value == other.input.value
    }
}

function sleepFor(sleepDuration) {
    let now = new Date().getTime();
    while (new Date().getTime() < now + sleepDuration) {
        continue
    }
}

function formIsValid() {
    let form = document.getElementById("registerForm")

    let email = new Field("email", "emailLabel")

    let phone = new Field("phone", "phoneLabel")
    let password = new Field("password", "passwordLabel")
    let passwordRepeat = new Field("passwordRepeat", "passwordRepeatLabel")

    let firstName = new Field("firstName", "firstNameLabel")
    let surname = new Field("surname", "surnameLabel")
    let lastName = new Field("lastName", "lastNameLabel")
    let address = new Field("address", "addressLabel")
    let department = new Field("department", "departmentLabel")

    let fields = [email, phone, password, passwordRepeat, firstName, surname, lastName, address, department]

    let invalid = []

    fields.forEach(field => {
        if (field.inputId == 'passwordRepeat')
            return
        if (field.isEmpty())
            invalid.push(field)
    })

    if (!password.valueIsEqual(passwordRepeat)) {
        invalid.push(passwordRepeat);
    }
    if (!password.isStrongPassword()) {
        invalid.push(password);
    }
    if (!phone.isPhone()) {
        invalid.push(phone);
    }
    if (!email.isEmail()) {
        invalid.push(email);
    }
    if (!department.isNumber()) {
        invalid.push(department);
    }


    invalid.forEach(field => {
        field.markAsInvalid()
    })

    function getValid(fields, invalid_) {
        let valid = []
        fields.forEach(field => {
            let fieldIsInvalid = false
            invalid_.forEach(invField => {
                if (field.id == invField.id) {
                    fieldIsInvalid = true
                    return
                }
            })
            if (!fieldIsInvalid)
                valid.push(field)
        })
        return valid
    }
    let valid = getValid(fields, invalid)
    valid.forEach(field => {
        if (password.isEmpty() && field.inputId == 'passwordRepeat') {
            return
        }
        field.markAsValid()
    })

    return invalid.length == 0;
}

// Example starter JavaScript for disabling form submissions if there are invalid fields
function bootstrapFormValidation() {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
}