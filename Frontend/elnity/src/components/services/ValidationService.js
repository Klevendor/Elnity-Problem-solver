const USER_REGEX = /^[a-zA-Z][a-zA-Z0-9-_\s]{2,30}$/;
const PASSWORD_REGEX = /[\w#$%!? \p{L}]{6,20}/u;
const EMAIL_REGEX = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/u;

export const ValidationService = {
     validateUserName(username){
        return USER_REGEX.test(username)
     },

     validatePassword(password){
        return PASSWORD_REGEX.test(password)
     },

     validateEmail(email){
        return EMAIL_REGEX.test(email)
     },
}