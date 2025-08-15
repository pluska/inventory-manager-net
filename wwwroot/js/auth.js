// Wait for Blazor to be ready
window.addEventListener("load", function () {
  // Check if Blazor is ready
  if (window.Blazor) {
    console.log("Blazor detected, initializing...");
    initializeApp();
  } else {
    // Wait for Blazor to be ready
    window.addEventListener("blazor:ready", function () {
      console.log("Blazor ready event fired, initializing...");
      initializeApp();
    });
  }
});

function initializeApp() {
  console.log("App initialized, all functions available");

  // Monitor Blazor connection - with proper error handling
  try {
    if (window.Blazor && window.Blazor.defaultReconnectionHandler) {
      console.log("Setting up Blazor connection monitoring...");
      window.Blazor.defaultReconnectionHandler._reconnectionDisplay = {
        show: function () {
          console.log("Blazor reconnecting...");
        },
        hide: function () {
          console.log("Blazor reconnected");
        },
      };
      console.log("Blazor connection monitoring set up successfully");
    } else {
      console.log("Blazor reconnection handler not available yet");
    }
  } catch (error) {
    console.log(
      "Error setting up Blazor connection monitoring:",
      error.message
    );
  }
}

// Authentication helper functions

// Global error handler to catch any JavaScript errors
window.addEventListener("error", function (e) {
  console.error("Global JavaScript error:", e.error);
  console.error(
    "Error details:",
    e.message,
    "at",
    e.filename,
    "line",
    e.lineno
  );
});

// Global unhandled promise rejection handler
window.addEventListener("unhandledrejection", function (e) {
  console.error("Unhandled promise rejection:", e.reason);
});

// Simple test function to verify JavaScript is working
function simpleTest() {
  alert("JavaScript is working!");
  console.log("simpleTest function called successfully");
}

// Test function that works with Blazor
function testBlazorConnection() {
  if (window.Blazor) {
    console.log("Blazor is available");
    return true;
  } else {
    console.log("Blazor is not available");
    return false;
  }
}

// Simple test function for Blazor to call
function testBlazorInterop() {
  console.log("Blazor JavaScript interop test successful!");
  return "JavaScript interop is working!";
}

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

// Enhanced logout function that accepts antiforgery token as parameter
function logoutUserWithToken(token) {
  console.log(
    "Logout function called with token:",
    token ? "Token present" : "No token"
  );

  // First, try to use an existing form if available
  const existingForm = document.getElementById("logoutForm");
  if (existingForm) {
    console.log("Using existing logout form");
    existingForm.submit();
    return;
  }

  // Fallback: Create a form and submit it to the logout endpoint
  const form = document.createElement("form");
  form.method = "POST";
  form.action = "/Account/Logout";

  // Add the provided antiforgery token
  if (token) {
    const tokenInput = document.createElement("input");
    tokenInput.type = "hidden";
    tokenInput.name = "__RequestVerificationToken";
    tokenInput.value = token;
    form.appendChild(tokenInput);
    console.log("Antiforgery token added to form");
  } else {
    console.log("No antiforgery token provided");
  }

  // Submit the form
  document.body.appendChild(form);
  console.log("Form submitted to:", form.action);
  form.submit();
}

// New function to perform logout properly
function performLogout() {
  console.log("Performing logout...");

  // Clear any stored authentication data
  if (window.localStorage) {
    window.localStorage.clear();
  }
  if (window.sessionStorage) {
    window.sessionStorage.clear();
  }

  // Clear any cookies related to authentication
  document.cookie.split(";").forEach(function (c) {
    document.cookie = c
      .replace(/^ +/, "")
      .replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/");
  });

  // Redirect to the Razor Page logout endpoint
  window.location.href = "/Account/Logout";
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
