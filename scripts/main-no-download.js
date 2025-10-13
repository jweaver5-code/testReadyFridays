// Quality Education Platform - Main JavaScript File with Authentication (No Downloads)

class QualityEducation {
  constructor() {
    this.currentUser = null;
    this.isAuthenticated = false;
    this.users = [];
    this.classrooms = [];
    this.systemLogs = [];
    this.db = null;
    
    this.currentCourse = null;
    this.currentModule = null;
    this.currentQuiz = null;
    this.quizAnswers = [];
    this.currentQuestionIndex = 0;
    
    this.subjects = [];
    this.courses = [];
    this.questions = {};
    
    this.init();
  }

  init() {
    this.loadData();
    this.setupEventListeners();
    this.showLogin();
  }

  loadData() {
    // Load subjects
    fetch('./data/subjects.json')
      .then(response => response.json())
      .then(data => {
        this.subjects = data;
        this.populateSubjectDropdowns();
      })
      .catch(error => console.error('Error loading subjects:', error));

    // Load courses
    fetch('./data/courses.json')
      .then(response => response.json())
      .then(data => {
        this.courses = data;
        this.populateCourseDropdowns();
      })
      .catch(error => console.error('Error loading courses:', error));

    // Load questions
    fetch('./data/questions.json')
      .then(response => response.json())
      .then(data => {
        this.questions = data;
      })
      .catch(error => console.error('Error loading questions:', error));

    // Load users from localStorage
    const savedUsers = localStorage.getItem('qualityEducationUsers');
    if (savedUsers) {
      this.users = JSON.parse(savedUsers);
    } else {
      // Initialize with default users
      this.initializeDefaultUsers();
    }

    // Load classrooms from localStorage
    const savedClassrooms = localStorage.getItem('qualityEducationClassrooms');
    if (savedClassrooms) {
      this.classrooms = JSON.parse(savedClassrooms);
    }

    // Load system logs from localStorage
    const savedLogs = localStorage.getItem('qualityEducationSystemLogs');
    if (savedLogs) {
      this.systemLogs = JSON.parse(savedLogs);
    }
  }

  initializeDefaultUsers() {
    this.users = [
      {
        id: 1,
        email: 'admin@qualityeducation.com',
        password: 'admin123',
        firstName: 'Admin',
        lastName: 'User',
        grade: 'Admin',
        role: 'admin',
        points: 0,
        completedModules: [],
        completedQuizzes: [],
        gamesPlayed: 0,
        recentActivity: [],
        createdAt: new Date().toISOString(),
        lastLogin: new Date().toISOString(),
        isActive: true
      }
    ];
    this.saveUsers();
  }

  saveUsers() {
    localStorage.setItem('qualityEducationUsers', JSON.stringify(this.users));
  }

  saveClassrooms() {
    localStorage.setItem('qualityEducationClassrooms', JSON.stringify(this.classrooms));
  }

  saveSystemLogs() {
    localStorage.setItem('qualityEducationSystemLogs', JSON.stringify(this.systemLogs));
  }

  setupEventListeners() {
    // Login form
    document.getElementById('loginForm').addEventListener('submit', (e) => {
      e.preventDefault();
      this.handleLogin();
    });

    // Logout button
    document.getElementById('logoutBtn').addEventListener('click', () => {
      this.logout();
    });

    // Navigation
    document.querySelectorAll('[data-section]').forEach(link => {
      link.addEventListener('click', (e) => {
        e.preventDefault();
        const section = e.target.getAttribute('data-section');
        this.showSection(section);
      });
    });

    // Admin tab navigation
    document.querySelectorAll('[data-admin-tab]').forEach(link => {
      link.addEventListener('click', (e) => {
        e.preventDefault();
        const tab = e.target.getAttribute('data-admin-tab');
        this.showAdminTab(tab);
      });
    });

    // Add user form (disabled for original index.html)
    // document.getElementById('addUserForm')?.addEventListener('submit', (e) => {
    //   e.preventDefault();
    //   this.addUser();
    // });

    // Add classroom form
    document.getElementById('addClassroomForm')?.addEventListener('submit', (e) => {
      e.preventDefault();
      this.addClassroom();
    });

    // Refresh data button
    document.getElementById('refreshData')?.addEventListener('click', () => {
      this.refreshData();
    });

    // Export database button (now refresh)
    document.getElementById('exportDatabase')?.addEventListener('click', () => {
      this.refreshData();
    });
  }

  handleLogin() {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;

    const user = this.users.find(u => u.email === email && u.password === password && u.isActive);

    if (user) {
      this.currentUser = user;
      this.isAuthenticated = true;
      user.lastLogin = new Date().toISOString();
      this.saveUsers();
      this.showDashboard();
      this.logActivity('User logged in', user.id);
    } else {
      this.showErrorMessage('Invalid email or password');
    }
  }

  logout() {
    this.currentUser = null;
    this.isAuthenticated = false;
    this.showLogin();
    this.logActivity('User logged out', null);
  }

  showLogin() {
    document.getElementById('login').classList.remove('d-none');
    document.getElementById('admin').classList.add('d-none');
    document.getElementById('teacher').classList.add('d-none');
    document.getElementById('student').classList.add('d-none');
    document.getElementById('userControls').classList.add('d-none');
  }

  showDashboard() {
    document.getElementById('login').classList.add('d-none');
    document.getElementById('userControls').classList.remove('d-none');
    
    if (this.currentUser.role === 'admin') {
      this.showAdminDashboard();
    } else if (this.currentUser.role === 'teacher') {
      this.showTeacherDashboard();
    } else if (this.currentUser.role === 'student') {
      this.showStudentDashboard();
    }
  }

  showAdminDashboard() {
    document.getElementById('admin').classList.remove('d-none');
    document.getElementById('teacher').classList.add('d-none');
    document.getElementById('student').classList.add('d-none');
    
    // Show admin navigation
    document.getElementById('adminNavItem').classList.remove('d-none');
    document.getElementById('adminEngagementNav').classList.remove('d-none');
    document.getElementById('adminClassroomsNav').classList.remove('d-none');
    document.getElementById('adminTeachersNav').classList.remove('d-none');
    document.getElementById('adminStudentsNav').classList.remove('d-none');
    
    this.updateAdminStats();
    this.loadAllUsers();
  }

  showTeacherDashboard() {
    document.getElementById('teacher').classList.remove('d-none');
    document.getElementById('admin').classList.add('d-none');
    document.getElementById('student').classList.add('d-none');
    
    // Show teacher navigation
    document.getElementById('teacherHomeNav').classList.remove('d-none');
    document.getElementById('teacherLessonsNav').classList.remove('d-none');
    document.getElementById('teacherClassroomNav').classList.remove('d-none');
    document.getElementById('teacherManageNav').classList.remove('d-none');
    document.getElementById('teacherActivitiesNav').classList.remove('d-none');
    document.getElementById('teacherReportsNav').classList.remove('d-none');
  }

  showStudentDashboard() {
    document.getElementById('student').classList.remove('d-none');
    document.getElementById('admin').classList.add('d-none');
    document.getElementById('teacher').classList.add('d-none');
    
    // Show student navigation
    document.getElementById('studentHomeNav').classList.remove('d-none');
    document.getElementById('studentActivitiesNav').classList.remove('d-none');
  }

  updateAdminStats() {
    const totalUsers = this.users.length;
    const teachers = this.users.filter(u => u.role === 'teacher').length;
    const students = this.users.filter(u => u.role === 'student').length;
    const totalPoints = this.users.reduce((sum, user) => sum + user.points, 0);

    document.getElementById('adminTotalUsers').textContent = totalUsers;
    document.getElementById('adminTeachers').textContent = teachers;
    document.getElementById('adminStudents').textContent = students;
    document.getElementById('adminTotalPoints').textContent = totalPoints;
  }

  loadAllUsers() {
    const tableBody = document.getElementById('adminUsersTable');
    if (!tableBody) return;

    tableBody.innerHTML = this.users.map(user => `
      <tr>
        <td><strong>${user.firstName} ${user.lastName}</strong></td>
        <td>${user.email}</td>
        <td><span class="badge bg-${user.role === 'admin' ? 'danger' : user.role === 'teacher' ? 'primary' : 'success'}">${user.role}</span></td>
        <td>${user.grade}</td>
        <td>${user.points}</td>
        <td>${new Date(user.lastLogin).toLocaleDateString()}</td>
        <td><span class="badge bg-${user.isActive ? 'success' : 'secondary'}">${user.isActive ? 'Active' : 'Inactive'}</span></td>
      </tr>
    `).join('');
  }

  addUser() {
    const firstName = document.getElementById('addUserFirstName').value;
    const lastName = document.getElementById('addUserLastName').value;
    const email = document.getElementById('addUserEmail').value;
    const password = document.getElementById('addUserPassword').value;
    const grade = document.getElementById('addUserGrade').value;
    const role = document.getElementById('addUserRole').value;

    if (!firstName || !lastName || !email || !password || !grade || !role) {
      this.showErrorMessage('Please fill in all fields');
      return;
    }

    if (role === 'admin') {
      this.showErrorMessage('Admin accounts cannot be created through this form');
      return;
    }

    if (this.users.find(u => u.email === email)) {
      this.showErrorMessage('User with this email already exists');
      return;
    }

    const newUser = {
      id: Date.now(),
      firstName,
      lastName,
      email,
      password,
      grade,
      role,
      points: 0,
      completedModules: [],
      completedQuizzes: [],
      gamesPlayed: 0,
      recentActivity: [],
      createdAt: new Date().toISOString(),
      lastLogin: new Date().toISOString(),
      isActive: true
    };

    this.users.push(newUser);
    this.saveUsers();
    this.loadAllUsers();
    this.updateAdminStats();
    this.showSuccessMessage('User added successfully');
    this.logActivity(`User added: ${firstName} ${lastName}`, this.currentUser.id);

    // Clear form
    document.getElementById('addUserForm').reset();
  }

  addClassroom() {
    const name = document.getElementById('addClassroomName').value;
    const grade = document.getElementById('addClassroomGrade').value;
    const subject = document.getElementById('addClassroomSubject').value;

    if (!name || !grade || !subject) {
      this.showErrorMessage('Please fill in all fields');
      return;
    }

    const newClassroom = {
      id: Date.now(),
      name,
      grade,
      subject,
      teacherId: this.currentUser.id,
      code: this.generateClassroomCode(),
      students: [],
      createdAt: new Date().toISOString(),
      isActive: true
    };

    this.classrooms.push(newClassroom);
    this.saveClassrooms();
    this.showSuccessMessage('Classroom created successfully');
    this.logActivity(`Classroom created: ${name}`, this.currentUser.id);

    // Clear form
    document.getElementById('addClassroomForm').reset();
  }

  generateClassroomCode() {
    return Math.random().toString(36).substring(2, 8).toUpperCase();
  }

  populateSubjectDropdowns() {
    const dropdowns = document.querySelectorAll('[id$="Subject"]');
    dropdowns.forEach(dropdown => {
      dropdown.innerHTML = '<option value="">Select Subject</option>' +
        this.subjects.map(subject => `<option value="${subject.name}">${subject.name}</option>`).join('');
    });
  }

  populateCourseDropdowns() {
    const dropdowns = document.querySelectorAll('[id$="Course"]');
    dropdowns.forEach(dropdown => {
      dropdown.innerHTML = '<option value="">Select Course</option>' +
        this.courses.map(course => `<option value="${course.title}">${course.title}</option>`).join('');
    });
  }

  showSection(section) {
    // Hide all sections
    document.querySelectorAll('.content-section').forEach(section => {
      section.classList.add('d-none');
    });

    // Show selected section
    const targetSection = document.getElementById(section);
    if (targetSection) {
      targetSection.classList.remove('d-none');
    }

    // Update navigation
    document.querySelectorAll('.nav-link').forEach(link => {
      link.classList.remove('active');
    });
    document.querySelector(`[data-section="${section}"]`).classList.add('active');
  }

  showAdminTab(tab) {
    // Hide all admin tabs
    document.querySelectorAll('.admin-tab').forEach(tab => {
      tab.classList.add('d-none');
    });

    // Show selected tab
    const targetTab = document.getElementById(`admin-${tab}`);
    if (targetTab) {
      targetTab.classList.remove('d-none');
    }

    // Update navigation
    document.querySelectorAll('[data-admin-tab]').forEach(link => {
      link.classList.remove('active');
    });
    document.querySelector(`[data-admin-tab="${tab}"]`).classList.add('active');
  }

  logActivity(message, userId) {
    const log = {
      id: Date.now(),
      timestamp: new Date().toISOString(),
      level: 'info',
      message,
      userId: userId || this.currentUser?.id || 'system'
    };

    this.systemLogs.push(log);
    this.saveSystemLogs();
  }

  showSuccessMessage(message) {
    // Create or update success message
    let alert = document.getElementById('successAlert');
    if (!alert) {
      alert = document.createElement('div');
      alert.id = 'successAlert';
      alert.className = 'alert alert-success alert-dismissible fade show position-fixed';
      alert.style.top = '20px';
      alert.style.right = '20px';
      alert.style.zIndex = '9999';
      document.body.appendChild(alert);
    }

    alert.innerHTML = `
      ${message}
      <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    // Auto-hide after 3 seconds
    setTimeout(() => {
      if (alert) {
        alert.remove();
      }
    }, 3000);
  }

  showErrorMessage(message) {
    // Create or update error message
    let alert = document.getElementById('errorAlert');
    if (!alert) {
      alert = document.createElement('div');
      alert.id = 'errorAlert';
      alert.className = 'alert alert-danger alert-dismissible fade show position-fixed';
      alert.style.top = '20px';
      alert.style.right = '20px';
      alert.style.zIndex = '9999';
      document.body.appendChild(alert);
    }

    alert.innerHTML = `
      ${message}
      <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    // Auto-hide after 5 seconds
    setTimeout(() => {
      if (alert) {
        alert.remove();
      }
    }, 5000);
  }

  refreshData() {
    this.loadData();
    if (this.isAuthenticated) {
      this.updateAdminStats();
      this.loadAllUsers();
    }
    this.showSuccessMessage('Data refreshed successfully');
  }
}

// Initialize the application
document.addEventListener('DOMContentLoaded', () => {
  new QualityEducation();
});
