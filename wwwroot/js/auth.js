// Authentication helper functions
function logoutUser() {
  // Create a form and submit it to the logout endpoint
  const form = document.createElement("form");
  form.method = "POST";
  form.action = "/Account/Logout";

  // Add antiforgery token if available
  const token = document.querySelector(
    'input[name="__RequestVerificationToken"]'
  )?.value;
  if (token) {
    const tokenInput = document.createElement("input");
    tokenInput.type = "hidden";
    tokenInput.name = "__RequestVerificationToken";
    tokenInput.value = token;
    form.appendChild(tokenInput);
  }

  // Submit the form
  document.body.appendChild(form);
  form.submit();
}

// Login user via API
async function loginUser(email, password, rememberMe) {
  try {
    // Create form data
    const formData = new FormData();
    formData.append("Email", email);
    formData.append("Password", password);
    formData.append("RememberMe", rememberMe.toString());

    // Send POST request to API
    const response = await fetch("/api/auth/login", {
      method: "POST",
      body: formData,
    });

    if (response.ok) {
      const result = await response.json();
      console.log("Login successful:", result);
      return true;
    } else {
      const error = await response.json();
      console.error("Login failed:", error);
      return false;
    }
  } catch (error) {
    console.error("Login error:", error);
    return false;
  }
}

// Get antiforgery token from the page
function getAntiforgeryToken() {
  // Try to get the token from a meta tag first
  let token = document.querySelector(
    'meta[name="__RequestVerificationToken"]'
  )?.content;

  if (!token) {
    // Fallback: try to get from an input field
    token = document.querySelector(
      'input[name="__RequestVerificationToken"]'
    )?.value;
  }

  if (!token) {
    // Last resort: try to get from the page content
    const tokenMatch = document.body.innerHTML.match(
      /name="__RequestVerificationToken" value="([^"]+)"/
    );
    token = tokenMatch ? tokenMatch[1] : null;
  }

  return token || "";
}

// Add antiforgery token to a form
function addTokenToForm(formId, token) {
  if (!token) return;

  const form = document.getElementById(formId);
  if (!form) return;

  // Check if token already exists
  let existingToken = form.querySelector(
    'input[name="__RequestVerificationToken"]'
  );
  if (existingToken) {
    existingToken.value = token;
  } else {
    // Create new token input
    const tokenInput = document.createElement("input");
    tokenInput.type = "hidden";
    tokenInput.name = "__RequestVerificationToken";
    tokenInput.value = token;
    form.appendChild(tokenInput);
  }
}
