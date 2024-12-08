# AWS SQS with Dead Letter Queue in ASP.NET Core

This project demonstrates how to integrate **AWS Simple Queue Service (SQS)** with a **Dead Letter Queue (DLQ)** using **ASP.NET Core**. It showcases handling message retries and gracefully moving failed messages to a DLQ.

---

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Project Structure](#project-structure)
- [Key Concepts](#key-concepts)
  - [What is a Dead Letter Queue?](#what-is-a-dead-letter-queue)
- [Configuration](#configuration)
- [Usage](#usage)
- [References](#references)

---

## Overview

Amazon Simple Queue Service (SQS) provides a reliable, fully-managed message queuing service for decoupling microservices or distributed systems. This project demonstrates:
- Sending and receiving messages via SQS.
- Configuring a **Dead Letter Queue (DLQ)** for unprocessed messages.
- Employing retry logic for transient errors.

---

## Features

- **AWS SDK for .NET** for seamless SQS integration.
- Configurable **Dead Letter Queue (DLQ)** to isolate failed messages.
- Retry logic for message processing.
- Logging and error handling using `ILogger`.

---

## Prerequisites

Before running this project, ensure you have:

1. **AWS Account**: [Sign up](https://aws.amazon.com/free/) if you don’t already have one.
2. **AWS CLI**: Installed and configured with your credentials. ([Guide](https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-install.html))
3. **.NET 6 SDK or later**: [Download](https://dotnet.microsoft.com/download/dotnet/6.0).
4. **Visual Studio/VS Code**: For editing and running the application.

---

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/yourrepository.git
   cd yourrepository
