const dialogue = document.querySelector(".dialogue")
function Register() {
    txt = $(event.target).attr("value")
    GetA(txt);
}
function Insert() {
    txt = document.getElementById('txtBox').value
    GetA(txt);
}
function QuestionButton() {
    Remotetext = `
                <div class="user remote">
                    <div class="avatar">
                        <div class="pic">
                            <img src="https://picsum.photos/100/100?random=12" />
                        </div>
                    <div class="name">漢克斯</div>
                </div>
                <div class="text">
                    <input class="btn" type="button" value="加入會員" onclick="Register()">
                    <input class="btn" type="button" value="我要掛號" onclick="Register()">
                    </br><input class="btn" type="button" value="交通指引" onclick="Register()">
                    <input  class="btn"type="button" value="衛教健保" onclick="Register()">
                    </br><input  class="btn"type="button" value="該掛哪科" onclick="WhichDep()">
                    <input class="btn" type="button" value="線上購物" onclick="Register()">
                </div>`
    dialogue.innerHTML += Remotetext;
    dialogue.scrollTop = dialogue.scrollHeight;
}
async function GetA(txt) {
    if (txt != "") {
        const Aws = await fetch('/doctor/GetAnswer' + `?Qs=${txt}`).then(response => response.json())
        console.log(Aws.answer)
        Usertext = `<div class="user local">
                            <div class="avatar">
                                <div class="pic">
                                    <img src="https://picsum.photos/100/100?random=16" />
                                </div>
                            <div class="name">Me</div>
                            </div>
                            <div class="text">${txt}</div>
                        </div>`;
        Remotetext = `
                <div class="user remote">
                    <div class="avatar">
                        <div class="pic">
                            <img src="https://picsum.photos/100/100?random=12" />
                        </div>
                    <div class="name">漢克斯</div>
                </div>
                <div class="text">
                    ${Aws.answer}
                </div>
        </div>`

        dialogue.innerHTML += Usertext;
        dialogue.innerHTML += Remotetext;
        dialogue.scrollTop = dialogue.scrollHeight;

    }
    else {
        alert("請輸入文字");
    }
}
async function WhichDep() {
    const txt = $(event.target).attr("value")
    Remotetext = `<div class="user remote">
                                <div class="avatar">
                                    <div class="pic">
                                        <img src="https://picsum.photos/100/100?random=12" />
                                    </div>
                                    <div class="name">漢克斯</div>
                                </div>
                                <div class="text">我們有服務的科目有\n`
    const deps = await fetch('/Admin/AdminDoctor/Dep').then(response => response.json())
    deps.forEach((deptName) => {
        Remotetext += `</br><input type="button" class="btn dep" value="${deptName}" onclick="Docbtn()">`
    })
    Usertext = `<div class="user local">
                            <div class="avatar">
                                <div class="pic">
                                    <img src="https://picsum.photos/100/100?random=16" />
                                </div>
                            <div class="name">Me</div>
                            </div>
                            <div class="text">${txt}</div>
                        </div>`;
    Remotetext += `</div>
                              </div>`
    dialogue.innerHTML += Usertext;
    dialogue.innerHTML += Remotetext;
    dialogue.scrollTop = dialogue.scrollHeight;
}
async function Docbtn() {
    depName = $(event.target).attr("value")
    const docs = await fetch('/Admin/AdminDoctor/Doc' + `?depName=${depName}`).then(response => response.json());
    Remotetext = `<div class="user remote">
                                <div class="avatar">
                                    <div class="pic">
                                        <img src="https://picsum.photos/100/100?random=12" />
                                    </div>
                                    <div class="name">漢克斯</div>
                                </div>
                                <div class="text">目前該科看診醫生有`
    docs.forEach((DoctorName) => {
        Remotetext += `</br><input class="btn doc" type="button" name="${docs[0].doctorId}" value="${DoctorName}" onclick="Trtbtn()">`
    })
    Usertext = `<div class="user local">
                            <div class="avatar">
                                <div class="pic">
                                    <img src="https://picsum.photos/100/100?random=16" />
                                </div>
                            <div class="name">Me</div>
                            </div>
                            <div class="text">${depName}</div>
                        </div>`;
    Remotetext += `</div>
                              </div>`
    dialogue.innerHTML += Usertext;
    dialogue.innerHTML += Remotetext;
    dialogue.scrollTop = dialogue.scrollHeight;
}
async function Trtbtn() {
    Remotetext = `<div class="user remote">
                                <div class="avatar">
                                    <div class="pic">
                                        <img src="https://picsum.photos/100/100?random=12" />
                                    </div>
                                    <div class="name">漢克斯</div>
                                </div>
                                <div class="text">該醫生的治療項有\n<div class="trt">`
    dcID = $(event.target).attr("value");
    console.log(dcID)
    const trts = await fetch('/doctor/getTreatmentByName' + `?doctorName=${dcID}`).then(response => response.json());
    console.log(trts)
    for (let i = 0; i < trts.length; i++) {
        Remotetext += `${trts[i].treatmentDetail1}</br>`
    }
    Usertext = `<div class="user local">
                            <div class="avatar">
                                <div class="pic">
                                    <img src="https://picsum.photos/100/100?random=16" />
                                </div>
                            <div class="name">Me</div>
                            </div>
                            <div class="text">${dcID}</div>
                        </div>`;
    Remotetext += `</div><a href="/Reserve/ReserveList?id=${trts[0].doctorId}" class="btn">我要掛號</a>` +
        `</div>
                              </div>`
    dialogue.innerHTML += Remotetext;
    dialogue.scrollTop = dialogue.scrollHeight;
}

/////機器人隱藏 / 展開
var x = false;
function show() {
    if (x == false) {
        document.querySelector('.upDialogue').style.bottom = "0%"
        x = true;
    }
    else {
        document.querySelector('.upDialogue').style.bottom = "-100%"
        x = false;
    }
}
////Demo
function demoone() {
     document.getElementById("txtBox").value = "眼睛痠痛"
}
function demotwo() {
    document.getElementById("txtBox").value = "乾眼症"
}