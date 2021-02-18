# TTC-Project-SocketServer

이 프로젝트는 TTC프로젝트의 클라이언트 동기화와 클라이언트와 소통과정에서 여러가지 연산을 수행하는 코드입니다..

<br/>

## Technique Used
```
Language : C#
framework : .Net
```
<br/>

## TTC-Project SocketServer의 구조

- Room 방식을 이용한 Client간의 동기화 범위

<img width="700" alt="SocketServer Structure" src="https://user-images.githubusercontent.com/46314169/106367931-b3043980-6389-11eb-8f54-071ef2db1b86.png"/>
<br/>

- Lobby에서 Server와 Client사이의 소통  

<img width="700" alt="SocketServer Structure" src="https://user-images.githubusercontent.com/46314169/106368685-ded5ee00-638e-11eb-90d3-dab1db99b09b.png"/>
<br/>

아래의 리스트는 구현된 기능에 대한 정리입니다.

```
1. 같은 Room 또는 Lobby에 위치한 Client간 채팅 기능.
2. Public/Private Room 접속 기능.
3. Room Owner 자동 재선정 기능.
4. Lobby에 위치한 Client들에게 RoomList Packet 전송.
5. API서버와 Profile데이터 송수신.
```

아래의 리스트는 구현 예정인 기능에 대한 정리입니다.

```
1. 각 Room에 별도로 인게임 동기화를 구현.
2. 유저의 고유 인벤토리 기능을 구현.
```

## Prerequisites

.... 작성 중....


## ETC..
이 프로젝트의 기본적인 구조에 대한 솔루션은 https://github.com/tom-weiland/tcp-udp-networking 을 참고했습니다.
