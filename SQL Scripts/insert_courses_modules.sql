USE unitech_college_db;

-- Insert Courses
INSERT INTO Courses (CourseName, CourseDescription) VALUES 
('Computer Science', 'Core computer science principles and programming.'),
('Software Engineering', 'Focus on software systems and engineering practices.'),
('Cyber Security', 'Protect systems and data from cyber threats.'),
('Data Science', 'Learn to analyse and interpret complex digital data.'),
('Artificial Intelligence', 'Build intelligent systems with AI techniques.');

-- Insert Modules for Computer Science (CourseID = 1)
INSERT INTO Modules (CourseID, ModuleCode, ModuleTitle, ModuleDescription) VALUES
(1, 'COS1903', 'Scala Programming', 'Functional programming using Scala.'),
(1, 'COS1920', 'Database Management', 'Design and implementation of databases.'),
(1, 'COS2905', 'Object Oriented Programming (Java)', 'OOP concepts using Java.'),
(1, 'COS2910', 'Database Management', 'Advanced database concepts.');

-- Modules for Software Engineering (CourseID = 2)
INSERT INTO Modules (CourseID, ModuleCode, ModuleTitle, ModuleDescription) VALUES
(2, 'SE3906', 'Interaction Design', 'User-centred design principles.'),
(2, 'SE3410', 'Web Application Penetration Testing', 'Web security testing techniques.'),
(2, 'SE3406', 'Fuzzy Logic & Knowledge Based Systems', 'Intelligent systems with fuzzy logic.'),
(2, 'SE3613', 'Data Mining', 'Extracting knowledge from data.');

-- Modules for Cyber Security (CourseID = 3)
INSERT INTO Modules (CourseID, ModuleCode, ModuleTitle, ModuleDescription) VALUES
(3, 'SE3901', 'C Programming', 'Low-level programming in C.'),
(3, 'SE3902', 'Computer Law and Cyber Security Management', 'Legal and ethical aspects of cyber security.'),
(3, 'SE3903', 'Linux Security', 'Securing Linux environments.'),
(3, 'SE3904', 'Cyber Threat Intelligence and Incident Response', 'Managing cyber threats.');

-- Modules for Data Science (CourseID = 4)
INSERT INTO Modules (CourseID, ModuleCode, ModuleTitle, ModuleDescription) VALUES
(4, 'DS4001', 'Introduction to Data Science', 'Fundamentals of data science.'),
(4, 'DS4002', 'Machine Learning', 'Supervised and unsupervised learning.'),
(4, 'DS4003', 'Big Data Analytics', 'Analysis of large-scale data sets.'),
(4, 'DS4004', 'Data Visualisation Techniques', 'Visualising data effectively.');

-- Modules for Artificial Intelligence (CourseID = 5)
INSERT INTO Modules (CourseID, ModuleCode, ModuleTitle, ModuleDescription) VALUES
(5, 'AI5001', 'Neural Networks', 'Deep learning and neural networks.'),
(5, 'AI5002', 'Natural Language Processing', 'Understanding human language.'),
(5, 'AI5003', 'Robotics and Automation', 'Building and programming robots.'),
(5, 'AI5004', 'AI Ethics and Governance', 'Ethical implications of AI systems.');

SELECT * FROM Courses;

SELECT * FROM Modules;

