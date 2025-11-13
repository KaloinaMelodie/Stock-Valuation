# .NET Project – Stock Management / Projet .NET – Gestion de Stock

## Introduction / Introduction
**EN:**  
The **Stock Management** project was developed to meet the needs of a company that wants to centralize the **management and tracking of its products** with different storage methods.  
The main goal is to **bring together in one platform** stock operations (entries, outputs), **automatic calculation of product costs**, and **visualization of sales statistics**.

**FR :**  
Le projet **Gestion de Stock** a été développé pour répondre aux besoins d'une entreprise souhaitant centraliser la **gestion et le suivi de ses produits** aux méthodes de stockage variées.  
L'objectif principal est de **rassembler sur une seule plateforme** les opérations de stock (entrées, sorties), le **calcul automatique du coût des produits**, et la **visualisation des statistiques de ventes**.

---

## Project Objective / Objectif du projet
**EN:**  
Efficient stock management is crucial for any business seeking to **optimize costs** and **maximize profits**.  
This system aims to:  
- Automate product entry and exit management.  
- Ensure accurate stock tracking based on the chosen valuation method.  
- Provide detailed statistical sales reports.

**FR :**  
Gérer efficacement les stocks est essentiel pour toute entreprise cherchant à **optimiser ses coûts** et **maximiser ses bénéfices**.  
Ce système vise à :  
- Automatiser la gestion des entrées et sorties de produits.  
- Assurer un suivi précis des stocks selon la méthode de valorisation choisie.  
- Fournir des rapports statistiques sur les ventes.

---

## Stock Management Methods Used / Méthodes de gestion de stock utilisées

| Method / Méthode | Description (EN) | Description (FR) | Use / Utilité |
|------------------|------------------|------------------|----------------|
| **FIFO (First In First Out)** | The first products that enter are the first to leave. | Les premiers produits entrés sont les premiers à sortir. | Ideal for perishable goods. / Idéal pour les produits périssables. |
| **LIFO (Last In First Out)** | The last products that enter are the first to leave. | Les derniers produits entrés sont les premiers à sortir. | Used in specific fiscal or industrial contexts. / Utilisé pour certains contextes fiscaux ou industriels. |
| **CMUP (Weighted Average Cost / Coût Moyen Unitaire Pondéré)** | Weighted average of purchase cost. | Moyenne pondérée du coût d’achat. | Provides stable stock valuation over time. / Permet une valorisation stable du stock dans le temps. |

---

## Main Features / Fonctionnalités principales

### Company Backoffice / Backoffice – Entreprise
**EN:**  
- **Product creation** with the choice of storage method (CMUP, FIFO, LIFO).  
- **Stock movement creation** (entry and output) based on the selected method.  
- **Stock sheet generation** showing entries, outputs, and current stock with dynamic costs.  
- **PDF export** of the stock sheet.  
- **CSV import** for external stock movement integration.

**FR :**  
- **Création de produit** avec sélection de la méthode de stockage (CMUP, FIFO, LIFO).  
- **Création de mouvements de stock** (entrée et sortie) selon la méthode choisie.  
- **Génération de la fiche de stock** : affichage des entrées, sorties et état du stock avec coûts variables.  
- **Export PDF de la fiche de stock.**  
- **Import CSV** pour intégrer des mouvements externes.

---

### Administrator Backoffice / Backoffice – Administrateur
**EN:**  
- **Reports and Statistics:**
  - Monthly revenue tracking.  

**FR :**  
- **Rapports et statistiques :**
  - Chiffre d’affaires par mois.  

---

## Technologies Used / Technologies utilisées

| Category / Catégorie | Details / Détails |
|----------------------|-------------------|
| Main framework / Framework principal | **.NET – Microsoft.NET.Sdk.Web** |
| Language / Langage | **C#** |
| Database / Base de données | **SQL Server** |
| Architecture | **ASP.NET Core Razor Pages** |
| ORM | **Entity Framework Core (with Identity)** |
| Export | **Rotativa.AspNetCore (wkhtmltopdf)** |
| CSV Import / Import CSV | **CsvHelper** |

---

## Screenshots / Captures d’écran

### Login Page / Page de connexion
![Login Page](./images/1_login_page.png)

### Product List / Liste des produits
![Product List](./images/2_liste_produit.png)

### Product Details / Détail d’un produit
![Product Details](./images/3_detail_produit.png)

### Stock Sheet / Fiche de stock
![Stock Sheet](./images/4_fiche_stock.png)

### Stock Movements / Liste des mouvements
![Stock Movements](./images/5_liste_mouvement.png)

### PDF Export / Export PDF de la fiche de stock
![PDF Export](./images/6_pdf_fiche_stock.png)

### Product Creation / Création d’un produit
![Product Creation](./images/7_creation_produit.png)

### Sales Statistics / Statistiques – Chiffre d’affaires
![Sales Statistics](./images/8_chiffre_affaire.png)

---

## Conceptual Data Model / Modèle conceptuel de données (MCD)
![MCD](./images/MCD.png)

---

## Conclusion / Conclusion
**EN:**  
The **Stock Management** project provides a complete solution to efficiently manage stock using various accounting methods, generate PDF reports, and monitor sales performance.  
In the next version, the system could include a **client management module** and extend its functionality to cover **sales management**.  
This evolution would allow the company to manage both internal stock and customer orders in one unified platform.

### Clients
- Manage customer orders:
  - Send **proforma requests**
  - Send **purchase orders**
  - View **completed orders**

### Sales Management
- Manage proforma requests:
  - Create **proformas** from client requests
  - View and track **client orders**

**FR :**  
Le projet **Gestion de Stock** offre une solution complète pour gérer efficacement les stocks selon plusieurs méthodes comptables, générer des rapports PDF, et suivre les performances des ventes.  
Dans la prochaine version, le système pourrait intégrer un **module de gestion des clients** et étendre ses fonctionnalités à la **gestion des ventes**.  
Cette évolution permettrait à l’entreprise de gérer à la fois le stock interne et les commandes clients sur une même plateforme.

### Clients
- Gestion des commandes :
  - Envoi de **demande de proformat**
  - Envoi de **bon de commande**
  - Consultation des **commandes effectuées**

### Gestion des ventes
- Gestion des demandes de proformat :
  - Création de **proformat** à partir des demandes clients
  - Consultation des **commandes clients**