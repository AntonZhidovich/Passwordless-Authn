document.getElementById("login-form").addEventListener("submit", handleLoginSubmit);

async function handleLoginSubmit(event) {
    event.preventDefault();
    let challengeString = atob(challengeInput);
    let challenge = new Uint8Array(challengeString.length);
    for (let i = 0; i < challengeString.length; i++) {
        challenge[i] = challengeString.charCodeAt(i);
    }

    let rpId = rpIdInput;
    let keys = keysInput;
    let allowCredentials = [];

    for (const element of keys) {
        let keyIdString = window.atob(element);
        let key = new Uint8Array(keyIdString.length);
        for (let i = 0; i < keyIdString.length; i++) {
            key[i] = keyIdString.charCodeAt(i);
        }
        allowCredentials.push({
            type: "public-key",
            id: key
        });
    }

    let as = {
        authenticatorAttachment: "platform",
        userVerification: "required"
    };

    let options = {
        challenge,
        timeout: 1800000,
        rpId,
        userVerification: "required",
        allowCredentials
    };

    if (options.allowCredentials.length == 0) {
        console.log("No registered credentials found.");
        return;
    }

    let result;

    try {
        result = await navigator.credentials.get({ publicKey: options });
    }
    catch (e) {
        console.log("Error in getting credentials.");
        return;
    }

    let encodedRes = {
        id: result.id,
        rawId:
            btoa(String.fromCharCode.apply(null, new Uint8Array(result.rawId))),
        type: result.type,
        response: {
            authenticatorData:
                btoa(String.fromCharCode.apply(null, new Uint8Array(result.response.authenticatorData))),
            signature:
                btoa(String.fromCharCode.apply(null, new Uint8Array(result.response.signature))),
            userHandle:
                btoa(String.fromCharCode.apply(null, new Uint8Array(result.response.userHandle))),
            clientDataJSON:
                btoa(String.fromCharCode.apply(null, new Uint8Array(result.response.clientDataJSON)))
        }
    }

    try {
        await sendResponce(encodedRes);
    }
    catch (error) {
        console.log("Error in sending data to server.");
        console.log(error);
    }
    console.log("The response is sent to the server.");
    window.location.href = "/";
}

async function sendResponce(encCreds) {
    let response = await fetch('/Fido/CompleteLogin', {
        method: 'POST',
        body: JSON.stringify(encCreds),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    });
    return response;
}