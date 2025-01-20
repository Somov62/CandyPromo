import axios from "axios";

const apiPrefix = "auth";

export async function login(email, password) {
    await axios
    .post(`/api/${apiPrefix}/login`, {email, password}
    )
    .then((response) => {
      console.log(response.data);
    }).catch((response) => {
      console.log(response.response.data.errors[0]);
    });
}

// function login() {
//   alert("hello");
// }
// export default login;
// function register() {}
