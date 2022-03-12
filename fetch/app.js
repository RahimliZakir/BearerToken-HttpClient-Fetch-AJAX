window.addEventListener("load", function () {
  const signInBearer = () => {
    let content = {
      username: "zakir",
      password: "zakir007",
    };

    let stringified = JSON.stringify(content);

    let fetchOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: stringified,
    };

    return fetch("https://localhost:7152/api/account", fetchOptions)
      .then((res) => res.json())
      .then((data) => sessionStorage.setItem("token", data.token))
      .catch((error) => this.sessionStorage.removeItem("token"));
  };

  signInBearer();

  getPeople("https://localhost:7152/api/people");

  async function getPeople(url) {
    let token = sessionStorage.getItem("token");
    const fetched = await fetch(url, {
      headers: new Headers({
        Authorization: `Bearer ${token}`,
      }),
    });
    const jsoned = await fetched.json();

    if (fetched.ok && fetched.status == 200) {
      console.log(jsoned);
    } else if (!fetched.ok && fetched.status == 401) {
      window.location.reload();
    } else {
      throw new Error("Error!");
    }
  }
});
