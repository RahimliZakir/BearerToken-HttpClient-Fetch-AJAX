$(function () {
  const signInBearer = () => {
    const obj = {
      username: "zakir",
      password: "zakir007",
    };

    const stringifiedObj = JSON.stringify(obj);

    $.ajax({
      url: "https://localhost:7152/api/account",
      type: "POST",
      data: stringifiedObj,
      contentType: "application/json",
      success: function (response) {
        if (!response.error) {
          sessionStorage.setItem("token", response.token);
          return;
        }

        console.log("Error!");
      },
      error: function (response) {
        console.log(response);
        sessionStorage.removeItem("token");
      },
    });
  };

  signInBearer();

  getPeople("https://localhost:7152/api/people");

  function getPeople(url) {
    const token = sessionStorage.getItem("token") || "";

    $.ajax({
      url: url,
      type: "GET",
      beforeSend: function (xhr) {
        xhr.setRequestHeader("Authorization", `Bearer ${token}`);
      },
      success: function (response) {
        console.log(response);
      },
      error: function (response, status, xhr) {
        if (response.status == 401) {
          window.location.reload();
        }

        alert(response);
      },
    });
  }
});
