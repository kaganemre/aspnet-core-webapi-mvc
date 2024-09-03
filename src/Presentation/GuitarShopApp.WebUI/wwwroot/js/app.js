const formElem = document.querySelector("form");

formElem.addEventListener("submit", async e => {
  e.preventDefault();
  let formData = new FormData();
  formData.append("file", document.getElementById("imageFile").files[0])
  // Image file is sent to Web API with FormData.
  const response = await fetch("http://localhost:5191/api/stream/upload-image", {
    method: "POST",
    body: formData
  });

  const result = await response.json();
  // Data in the HTML form is copied to FormData.
  let data = new FormData(formElem);
  // The Image file name in the response from the API is added to FormData.
  data.set("Image", result["randomFileName"]);

  // After the model information is filled in, the model is sent to the Create action method via the AJAX(Fetch API).
  try {
    await fetch(uri, {
      method: "POST",
      body: data
    });
    window.location.href = "/products";
  } catch (error) {
    console.error("Error:", error);
  }
});