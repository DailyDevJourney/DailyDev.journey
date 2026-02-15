OneDayOneDev – Task Manager (C# / WinForms)
🎯 Objectif

Projet réalisé dans le cadre d’un challenge personnel :
30 jours – 30 défis C# consécutifs autour d’un projet unique évolutif.

L’objectif est de :

Maintenir et faire progresser mon niveau technique

Approfondir des concepts d’architecture

Construire un projet structuré démontrant une montée en compétences

Ce projet a commencé comme une application console simple et évolue progressivement vers une architecture plus propre et plus robuste.

🔍 Contexte

Ce challenge s’inscrit dans une période de transition professionnelle.

Plutôt que de rester passif, j’ai choisi de structurer mon temps autour d’un objectif concret : améliorer mes compétences en C# et renforcer mon profil backend/full stack.

Chaque évolution du projet reflète une volonté d’amélioration continue : refactoring, séparation des responsabilités, introduction de patterns et amélioration de l’architecture.

🏗 Architecture

Le projet a évolué en plusieurs étapes :

Application console initiale

Création du modèle TaskItem

Mise en place d’un système de logging

Export CSV des tâches

Introduction des tests unitaires (xUnit)

Création d’un TaskService pour séparer l’IHM de la logique métier

Migration vers SQLite via DbContext

Implémentation d’un Repository

Ajout d’une interface WinForms

Implémentation d’un système complet d’Undo/Redo

Architecture actuelle

Presentation (WinForms)
→ Application (TaskService + CommandManager)
→ Infrastructure (Repository + SQLite)

🔄 Undo / Redo – Pattern Command

Implémentation d’un système complet de retour en arrière via :

Interface ICommand

CommandManager avec double stack (Undo / Redo)

Commandes dédiées :

AddTaskCommand

DeleteTaskCommand

UpdateTaskCommand

CompleteTaskCommand

Gestion d’état via système de snapshot (Clone) pour garantir la cohérence des retours arrière.

🧠 Concepts techniques utilisés

Interfaces

Injection de dépendances simple

Pattern Command

Repository pattern

Séparation des responsabilités

Gestion d’état

Logging

DateTimeProvider

Tests unitaires (xUnit)

SQLite

📺 Suivi du challenge

L’évolution du projet est documentée en vidéo dans le cadre du challenge
OneDayOneDev.
Les vidéos sont de formats courts (ce ne sont pas des shorts).
Ce choix est volontaire

👉 https://www.youtube.com/@OneDayOneDev

🚀 Évolutions possibles

Migration vers WPF

Frontend web (Vue.js / Angular)

API REST

Amélioration de l’IHM

Augmentation de la couverture de tests
