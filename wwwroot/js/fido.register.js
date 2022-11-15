
document.getElementById("register-form").addEventListener("submit", handleRegisterSubmit);

async function handleRegisterSubmit(event) {
    event.preventDefault();
    let challengeBytesAsString = atob(challengeInput);
    let challenge = new Uint8Array(challengeBytesAsString.length);
    for (let i = 0; i < challengeBytesAsString.length; i++) {
        challenge[i] = challengeBytesAsString.charCodeAt(i);
    }
    const pbc = [
        { type: "public-key", alg: -7 },
        { type: "public-key", alg: -257 },
    ];
    let rp = { id: relPartyInput, name: "Fido test server" };
    let userHandleBytes = atob(userHandleInput);
    let userHandle = new Uint8Array(userHandleBytes.length);
    for (let i = 0; i < userHandleBytes.length; i++) {
        userHandle[i] = userHandleBytes.charCodeAt(i);
    }
    let user = {
        name: userIdInput,
        displayName: displayNameInput,
        id: userHandle
    };
    console.log(user.displayName);
    let as = {
        authenticatorAttachment: "platform",
        userVerification: "required"
    };

    let options = {
        rp,
        user,
        challenge,
        pubKeyCredParams: pbc,
        timeout: 1800000,
        attestation: "none",
        authenticatorSelection: as,
    }

    let credentials;

    try {
        credentials = await navigator.credentials.create({ publicKey: options });
    }
    catch (e) {
        console.log("Error in creating credentials.");
        console.log(e);
        return;
    }

    let encCreds = {
        id: credentials.id,
        rawId: btoa(String.fromCharCode.apply(null, new Uint8Array(credentials.rawId))),
        type: credentials.type,
        response: {
            attestationObject:
                btoa(String.fromCharCode.apply(null, new Uint8Array(credentials.response.attestationObject))),
            clientDataJSON:
                btoa(String.fromCharCode.apply(null, new Uint8Array(credentials.response.clientDataJSON)))
        }
    };

    try {
        response = await sendResponce(encCreds);
        window.location.href = "/";
    }
    catch (e) {
        console.log(e);
        return;
    }
    console.log("Credential Object", response);
}

async function sendResponce(encCreds) {
    let response = await fetch('/Fido/CompleteRegistration', {
        method: 'POST',
        body: JSON.stringify(encCreds),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    });
    return response;
}